using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Entities;

namespace Moasher.Application.Features.Entities.EventHandlers;

public class EntityUpdatedEventHandler : INotificationHandler<EntityUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public EntityUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EntityUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var entityId = notification.Entity.Id;
        var entity = await _context.Entities
            .Include(e => e.Initiatives).ThenInclude(i => i.ApprovedCosts)
            .Include(e => e.Initiatives).ThenInclude(i => i.Budgets)
            // .Include(e => e.Initiatives).ThenInclude(i => i.Contracts).ThenInclude(c => c.Expenditures)
            .Include(e => e.Initiatives).ThenInclude(i => i.Deliverables)
            .Include(e => e.Initiatives).ThenInclude(i => i.Issues)
            .Include(e => e.Initiatives).ThenInclude(i => i.Milestones)
            .Include(e => e.Initiatives).ThenInclude(i => i.Risks)
            .Include(e => e.Initiatives).ThenInclude(i => i.Teams)
            .Include(e => e.KPIs).ThenInclude(k => k.Values)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken);

        if (entity is not null)
        {
            entity.Initiatives.ToList().ForEach(i => 
            {
                i.Entity = entity;
                i.ApprovedCosts.ToList().ForEach(a => a.Initiative = i);
                i.Budgets.ToList().ForEach(b => b.Initiative = i);
                // i.Contracts.ToList().ForEach(c =>
                // {
                //     c.Initiative = i;
                //     c.Expenditures.ToList().ForEach(e => e.Initiative = i);
                // });
                i.Deliverables.ToList().ForEach(d => d.Initiative = i);
                i.Issues.ToList().ForEach(issue => issue.Initiative = i);
                i.Milestones.ToList().ForEach(m => m.Initiative = i);
                i.Risks.ToList().ForEach(r => r.Initiative = i);
                i.Teams.ToList().ForEach(t => t.Initiative = i);
            });
        
            entity.KPIs.ToList().ForEach(k =>
            {
                k.Entity = entity;
                k.Values.ToList().ForEach(v => v.KPI = k);
            });
        
            _context.Initiatives.UpdateRange(entity.Initiatives);
            _context.KPIs.UpdateRange(entity.KPIs);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}