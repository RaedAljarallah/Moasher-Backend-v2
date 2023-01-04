using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Initiatives;

namespace Moasher.Application.Features.Initiatives.EventHandlers;

public class InitiativeUpdatedEventHandler : INotificationHandler<InitiativeUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public InitiativeUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(InitiativeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.Initiative.Id;
        
        var initiative = await _context.Initiatives
            .IgnoreQueryFilters()
            .Include(i => i.ApprovedCosts)
            .Include(i => i.Budgets)
            .Include(i => i.Contracts)
            .Include(i => i.Projects)
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
            initiative.Contracts.ToList().ForEach(c => c.Initiative = initiative);
            initiative.Projects.ToList().ForEach(p => p.Initiative = initiative);
            initiative.Deliverables.ToList().ForEach(d => d.Initiative = initiative);
            initiative.Impacts.ToList().ForEach(i => i.Initiative = initiative);
            initiative.Issues.ToList().ForEach(i => i.Initiative = initiative);
            initiative.Milestones.ToList().ForEach(i => i.Initiative = initiative);
            initiative.Risks.ToList().ForEach(r => r.Initiative = initiative);
            initiative.Teams.ToList().ForEach(t => t.Initiative = initiative);
            initiative.Analytics.ToList().ForEach(a => a.Initiative = initiative);
            
            _context.Initiatives.Update(initiative);
            
            var searchRecord = await _context.SearchRecords
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.RelativeId == initiativeId, cancellationToken);
            if (searchRecord is not null)
            {
                searchRecord.Title = initiative.Name;
            }
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}