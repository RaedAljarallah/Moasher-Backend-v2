using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelTwoStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelTwoStrategicObjectiveUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public LevelTwoStrategicObjectiveUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(LevelTwoStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var strategicObjective = notification.StrategicObjective;

        var initiatives = await _context.Initiatives
            .Where(i => i.LevelTwoStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        var kpis = await _context.KPIs
            .Where(k => k.LevelTwoStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        initiatives.ForEach(i => i.LevelTwoStrategicObjective = strategicObjective);
        kpis.ForEach(k => k.LevelTwoStrategicObjective = strategicObjective);

        _context.Initiatives.UpdateRange(initiatives);
        _context.KPIs.UpdateRange(kpis);
        await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
    }
}