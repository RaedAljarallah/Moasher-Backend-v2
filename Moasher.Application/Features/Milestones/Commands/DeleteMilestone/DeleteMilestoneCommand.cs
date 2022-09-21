using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Milestones;

namespace Moasher.Application.Features.Milestones.Commands.DeleteMilestone;

public record DeleteMilestoneCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteMilestoneCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _context.InitiativeMilestones
            //.Include(m => m.ContractMilestones)
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (milestone is null)
        {
            throw new NotFoundException();
        }
    
        // _context.ContractMilestones.RemoveRange(milestone.ContractMilestones);
        milestone.AddDomainEvent(new MilestoneDeletedEvent(milestone));
        _context.InitiativeMilestones.Remove(milestone);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}