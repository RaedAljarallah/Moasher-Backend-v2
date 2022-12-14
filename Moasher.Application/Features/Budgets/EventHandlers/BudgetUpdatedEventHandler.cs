using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Events.Budgets;

namespace Moasher.Application.Features.Budgets.EventHandlers;

public class BudgetUpdatedEventHandler : INotificationHandler<BudgetUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public BudgetUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(BudgetUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.Budget.InitiativeId;
        var initiative = await _context.Initiatives.Include(i => i.Budgets)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);
        if (initiative is not null)
        {
            initiative.SetTotalBudget();
            initiative.SetCurrentYearBudget();
            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}