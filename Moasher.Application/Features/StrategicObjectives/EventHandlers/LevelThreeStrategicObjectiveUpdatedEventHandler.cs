using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelThreeStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelThreeStrategicObjectiveUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public LevelThreeStrategicObjectiveUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(LevelThreeStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var strategicObjectiveId = notification.StrategicObjective.Id;
        
        var strategicObjective = await _context.StrategicObjectives
            .Include(o => o.Initiatives)
            .Include(o => o.KPIs)
            .FirstOrDefaultAsync(o => o.Id == strategicObjectiveId, cancellationToken);

        if (strategicObjective is not null)
        {
            strategicObjective.Initiatives.ToList().ForEach(i => i.LevelThreeStrategicObjective = strategicObjective);
            strategicObjective.KPIs.ToList().ForEach(k => k.LevelThreeStrategicObjective = strategicObjective);

            _context.Initiatives.UpdateRange(strategicObjective.Initiatives);
            _context.KPIs.UpdateRange(strategicObjective.KPIs);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}