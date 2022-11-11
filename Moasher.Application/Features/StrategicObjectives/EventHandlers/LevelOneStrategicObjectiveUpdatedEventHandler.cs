using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelOneStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelOneStrategicObjectiveUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public LevelOneStrategicObjectiveUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(LevelOneStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var strategicObjective = notification.StrategicObjective;

        var initiatives = await _context.Initiatives
            .Where(i => i.LevelOneStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        var kpis = await _context.KPIs
            .Where(k => k.LevelOneStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);


        initiatives.ForEach(i => i.LevelOneStrategicObjective = strategicObjective);
        kpis.ForEach(k => k.LevelOneStrategicObjective = strategicObjective);
            
        _context.Initiatives.UpdateRange(initiatives);
        _context.KPIs.UpdateRange(kpis);
        await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
    }
}