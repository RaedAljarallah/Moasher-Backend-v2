using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelFourStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelFourStrategicObjectiveUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LevelFourStrategicObjectiveUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(LevelFourStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();

        var strategicObjective = notification.StrategicObjective;

        var initiatives = await context.Initiatives
            .Where(i => i.LevelFourStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        var kpis = await context.KPIs
            .Where(k => k.LevelFourStrategicObjectiveId == strategicObjective.Id)
            .ToListAsync(cancellationToken);

        initiatives.ForEach(i => i.LevelFourStrategicObjective = strategicObjective);
        kpis.ForEach(k => k.LevelFourStrategicObjective = strategicObjective);

        await context.SaveChangesAsync(cancellationToken);
    }
}