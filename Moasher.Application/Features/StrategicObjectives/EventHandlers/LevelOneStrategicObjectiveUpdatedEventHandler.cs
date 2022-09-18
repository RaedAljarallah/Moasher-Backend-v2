using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelOneStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelOneStrategicObjectiveUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LevelOneStrategicObjectiveUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(LevelOneStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        
        var strategicObjective = notification.StrategicObjective;

        var initiatives = await context.Initiatives
            .Where(i => i.LevelOneStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        var kpis = await context.KPIs
            .Where(k => k.LevelOneStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);


        initiatives.ForEach(i => i.LevelOneStrategicObjective = strategicObjective);
        kpis.ForEach(k => k.LevelOneStrategicObjective = strategicObjective);
            
        await context.SaveChangesAsync(cancellationToken);
    }
}