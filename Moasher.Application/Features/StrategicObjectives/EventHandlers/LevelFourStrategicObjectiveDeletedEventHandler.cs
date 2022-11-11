using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelFourStrategicObjectiveDeletedEventHandler : INotificationHandler<LevelFourStrategicObjectiveDeletedEvent>
{
    private readonly IMoasherDbContext _context;

    public LevelFourStrategicObjectiveDeletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(LevelFourStrategicObjectiveDeletedEvent notification, CancellationToken cancellationToken)
    {
        var strategicObjectiveId = notification.StrategicObjective.Id;

        var initiatives = await _context.Initiatives
            .Where(i => i.LevelFourStrategicObjectiveId == strategicObjectiveId)
            .ToListAsync(cancellationToken);

        var kpis = await _context.KPIs
            .Where(k => k.LevelFourStrategicObjectiveId == strategicObjectiveId)
            .ToListAsync(cancellationToken);

        initiatives.ForEach(i => i.LevelFourStrategicObjective = null);
        kpis.ForEach(k => k.LevelFourStrategicObjective = null);
        
        _context.Initiatives.UpdateRange(initiatives);
        _context.KPIs.UpdateRange(kpis);
        await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
    }
}