using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelTwoStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelTwoStrategicObjectiveUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LevelTwoStrategicObjectiveUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(LevelTwoStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        
        var strategicObjective = notification.StrategicObjective;

        var initiatives = await context.Initiatives
            .Where(i => i.LevelTwoStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        var kpis = await context.KPIs
            .Where(k => k.LevelTwoStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        initiatives.ForEach(i => i.LevelTwoStrategicObjective = strategicObjective);
        kpis.ForEach(k => k.LevelTwoStrategicObjective = strategicObjective);

        await context.SaveChangesAsync(cancellationToken);
    }
}