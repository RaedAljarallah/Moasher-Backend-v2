using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Initiatives;
using Moasher.Domain.Extensions;

namespace Moasher.Application.Features.Initiatives.EventHandlers;

public class InitiativeStatusUpdateEventHandler : INotificationHandler<InitiativeStatusUpdateEvent>
{
    private readonly IMoasherDbContext _context;

    public InitiativeStatusUpdateEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task Handle(InitiativeStatusUpdateEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.Initiative.Id;

        var initiative = await _context.Initiatives
            .Include(i => i.Milestones)
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);

        if (initiative is not null)
        {
            if (initiative.Milestones.Any())
            {
                var statusEnums = await _context.EnumTypes
                    .Where(e => e.Category == EnumTypeCategory.InitiativeStatus.ToString())
                    .ToListAsync(cancellationToken);

                initiative.SetStatus(statusEnums);
            }
            else
            {
                initiative.StatusEnum = null;
            }

            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}