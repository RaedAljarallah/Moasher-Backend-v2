using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelFourStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelFourStrategicObjectiveUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public LevelFourStrategicObjectiveUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(LevelFourStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var strategicObjective = notification.StrategicObjective;

        var initiatives = await _context.Initiatives
            .Where(i => i.LevelFourStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        var kpis = await _context.KPIs
            .Where(k => k.LevelFourStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        initiatives.ForEach(i => i.LevelFourStrategicObjective = strategicObjective);
        kpis.ForEach(k => k.LevelFourStrategicObjective = strategicObjective);

        await _context.SaveChangesAsync(cancellationToken);
    }
}