using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Events.Milestones;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Milestones.Commands.CreateMilestone;

public record CreateMilestoneCommand : MilestoneCommandBase, IRequest<MilestoneDto>;

public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, MilestoneDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateMilestoneCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MilestoneDto> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Milestones)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        request.ValidateAndThrow(new MilestoneDomainValidator(initiative, request.Name, request.Weight,
            request.PlannedFinish, request.ActualFinish));
        
        var milestone = _mapper.Map<InitiativeMilestone>(request);
        milestone.Initiative = initiative;
        milestone.AddDomainEvent(new MilestoneCreatedEvent(milestone));
        initiative.Milestones.Add(milestone);
        
        milestone.SetEditRequest(EditRequest.CreateRequest(nameof(CreateMilestoneCommand)));
        
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MilestoneDto>(milestone);
    }
}