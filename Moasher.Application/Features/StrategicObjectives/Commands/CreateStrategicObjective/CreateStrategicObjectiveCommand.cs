using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Application.Features.StrategicObjectives.Queries.GetStrategicObjectives;
using Moasher.Domain.Entities.StrategicObjectiveEntities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.StrategicObjectives.Commands.CreateStrategicObjective;

public record CreateStrategicObjectiveCommand : StrategicObjectiveCommandBase, IRequest<object>
{
    
}

public class CreateStrategicObjectiveCommandHandler : IRequestHandler<CreateStrategicObjectiveCommand, object>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _handler;

    public CreateStrategicObjectiveCommandHandler(IMoasherDbContext context, IMapper mapper, ISender handler)
    {
        _context = context;
        _mapper = mapper;
        _handler = handler;
    }
    
    public async Task<object> Handle(CreateStrategicObjectiveCommand request, CancellationToken cancellationToken)
    {
        var strategicObjectives = await _context.StrategicObjectives
            .Where(o => o.HierarchyId.GetLevel() == HierarchyIdService.Parse(request.HierarchyId).GetLevel())
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        request.ValidateAndThrow(new StrategicObjectiveDomainValidator(strategicObjectives, request.Name, request.Code));
        
        var strategicObjective = _mapper.Map<StrategicObjective>(request);
        _context.StrategicObjectives.Add(strategicObjective);
        await _context.SaveChangesAsync(cancellationToken);
        
        var createdStrategicObjectiveDetails = await _handler.Send(new GetStrategicObjectivesQuery 
        { 
            Code = strategicObjective.Code,
            Level = strategicObjective.HierarchyId.GetLevel()
        }, cancellationToken);

        return ((IEnumerable<object>) createdStrategicObjectiveDetails).FirstOrDefault()!;
    }
}