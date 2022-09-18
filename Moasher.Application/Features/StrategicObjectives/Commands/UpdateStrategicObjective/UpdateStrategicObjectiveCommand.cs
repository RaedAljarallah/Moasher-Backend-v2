using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Application.Features.StrategicObjectives.Queries.GetStrategicObjectives;
using Moasher.Domain.Events.StrategicObjectives;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.StrategicObjectives.Commands.UpdateStrategicObjective;

public record UpdateStrategicObjectiveCommand : StrategicObjectiveCommandBase, IRequest<object>
{
    public Guid Id { get; set; }
}

public class UpdateStrategicObjectiveCommandHandler : IRequestHandler<UpdateStrategicObjectiveCommand, object>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _handler;

    public UpdateStrategicObjectiveCommandHandler(IMoasherDbContext context, IMapper mapper, ISender handler)
    {
        _context = context;
        _mapper = mapper;
        _handler = handler;
    }

    public async Task<object> Handle(UpdateStrategicObjectiveCommand request, CancellationToken cancellationToken)
    {
        var strategicObjectives = await _context.StrategicObjectives
            .Where(o => o.HierarchyId.GetLevel() == HierarchyIdService.Parse(request.HierarchyId).GetLevel())
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var strategicObjective = strategicObjectives.FirstOrDefault(o => o.Id == request.Id);
        if (strategicObjective is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new StrategicObjectiveDomainValidator(
            strategicObjectives.Where(o => o.Id != request.Id).ToList(), request.Name, request.Code));

        var hasEvent = request.Name != strategicObjective.Name;
        
        _mapper.Map(request, strategicObjective);
        if (hasEvent)
        {
            switch (request.Level)
            {
                case 1:
                    strategicObjective.AddDomainEvent(new LevelOneStrategicObjectiveUpdatedEvent(strategicObjective));
                    break;
                case 2:
                    strategicObjective.AddDomainEvent(new LevelTwoStrategicObjectiveUpdatedEvent(strategicObjective));
                    break;
                case 3:
                    strategicObjective.AddDomainEvent(new LevelThreeStrategicObjectiveUpdatedEvent(strategicObjective));
                    break;
                case 4:
                    strategicObjective.AddDomainEvent(new LevelFourStrategicObjectiveUpdatedEvent(strategicObjective));
                    break;
            }
        }

        _context.StrategicObjectives.Update(strategicObjective);
        await _context.SaveChangesAsync(cancellationToken);
        
        var updatedStrategicObjectiveDetails = await _handler.Send(new GetStrategicObjectivesQuery
        {
            Code = strategicObjective.Code,
            Level = strategicObjective.HierarchyId.GetLevel()
        }, cancellationToken);
        
        return ((IEnumerable<object>) updatedStrategicObjectiveDetails).FirstOrDefault()!;
    }
}