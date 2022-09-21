using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Milestones;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Milestones.Commands.UpdateMilestone;

public record UpdateMilestoneCommand : MilestoneCommandBase, IRequest<MilestoneDto>
{
    public Guid Id { get; set; }
}

public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand, MilestoneDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateMilestoneCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MilestoneDto> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Milestones)
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var milestone = initiative.Milestones.FirstOrDefault(m => m.Id == request.Id);
        if (milestone is null)
        {
            throw new NotFoundException();
        }

        initiative.Milestones = initiative.Milestones.Where(m => m.Id != request.Id).ToList();
        request.ValidateAndThrow(new MilestoneDomainValidator(initiative, request.Name, request.Weight,
            request.PlannedFinish, request.ActualFinish));

        _mapper.Map(request, milestone);
        milestone.AddDomainEvent(new MilestoneUpdatedEvent(milestone));
        _context.InitiativeMilestones.Update(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MilestoneDto>(milestone);
    }
}