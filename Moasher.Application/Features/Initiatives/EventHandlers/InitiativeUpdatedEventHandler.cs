using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Initiatives;

namespace Moasher.Application.Features.Initiatives.EventHandlers;

public class InitiativeUpdatedEventHandler : INotificationHandler<InitiativeUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InitiativeUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(InitiativeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        var initiativeId = notification.Initiative.Id;
        
        var initiative = await context.Initiatives
            .Include(i => i.ApprovedCosts)
            .Include(i => i.Budgets)
            // .Include(i => i.Contracts)
            // .ThenInclude(c => c.Expenditures)
            .Include(i => i.Deliverables)
            .Include(i => i.Impacts)
            .Include(i => i.Issues)
            .Include(i => i.Milestones)
            .Include(i => i.Risks)
            .Include(i => i.Teams)
            .Include(i => i.Analytics)
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);

        if (initiative is not null)
        {
            initiative.ApprovedCosts.ToList().ForEach(a => a.Initiative = initiative);
            initiative.Budgets.ToList().ForEach(b => b.Initiative = initiative);
            // initiative.Contracts.ToList().ForEach(c =>
            // {
            //     c.Initiative = initiative;
            //     c.Expenditures.ToList().ForEach(e =>
            //     {
            //         e.Contract = c;
            //         e.Initiative = initiative;
            //     });
            // });
            initiative.Deliverables.ToList().ForEach(d => d.Initiative = initiative);
            initiative.Impacts.ToList().ForEach(i => i.Initiative = initiative);
            initiative.Issues.ToList().ForEach(i => i.Initiative = initiative);
            initiative.Milestones.ToList().ForEach(i => i.Initiative = initiative);
            initiative.Risks.ToList().ForEach(r => r.Initiative = initiative);
            initiative.Teams.ToList().ForEach(t => t.Initiative = initiative);
            initiative.Analytics.ToList().ForEach(a => a.Initiative = initiative);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}