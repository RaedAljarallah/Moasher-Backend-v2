using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Milestones;

namespace Moasher.Application.Features.Milestones.EventHandlers;

public class MilestoneCreatedEventHandler : INotificationHandler<MilestoneCreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public MilestoneCreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(MilestoneCreatedEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.Milestone.InitiativeId;

        var initiative = await _context.Initiatives.Include(i => i.Milestones)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);
        
        if (initiative is not null)
        {
            initiative.SetProgress();
            if (initiative.CalculateStatus)
            {
                var status = await _context.EnumTypes
                    .Where(e => e.Category.ToLower() == EnumTypeCategory.InitiativeStatus.ToString().ToLower())
                    .IgnoreQueryFilters()
                    .ToListAsync(cancellationToken);

                initiative.SetStatus(status);
            }

            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}