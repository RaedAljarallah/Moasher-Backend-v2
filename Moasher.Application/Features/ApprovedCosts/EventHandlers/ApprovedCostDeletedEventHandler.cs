using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Events.ApprovedCosts;

namespace Moasher.Application.Features.ApprovedCosts.EventHandlers;

public class ApprovedCostDeletedEventHandler : INotificationHandler<ApprovedCostDeletedEvent>
{
    private readonly IMoasherDbContext _context;

    public ApprovedCostDeletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ApprovedCostDeletedEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.ApprovedCost.InitiativeId;
        var initiative = await _context.Initiatives.Include(i => i.ApprovedCosts)
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);
        if (initiative is not null)
        {
            initiative.SetTotalApprovedCost();
            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}