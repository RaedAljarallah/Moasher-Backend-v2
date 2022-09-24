﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Milestones;
using Moasher.Domain.Extensions;

namespace Moasher.Application.Features.Milestones.EventHandlers;

public class MilestoneDeletedEventHandler : INotificationHandler<MilestoneDeletedEvent>
{
    private readonly IMoasherDbContext _context;

    public MilestoneDeletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task Handle(MilestoneDeletedEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.Milestone.InitiativeId;
        var initiative = await _context.Initiatives.Include(i => i.Milestones)
            .FirstOrDefaultAsync(i => i.Id == initiativeId, cancellationToken);

        if (initiative is not null)
        {
            initiative.SetProgress();
            if (initiative.CalculateStatus)
            {
                var status = await _context.EnumTypes
                    .Where(e => e.Category == EnumTypeCategory.InitiativeStatus.ToString())
                    .ToListAsync(cancellationToken);

                initiative.SetStatus(status);
            }
            _context.Initiatives.Update(initiative);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}