using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelFourStrategicObjectiveDeletedEventHandler : INotificationHandler<LevelFourStrategicObjectiveDeletedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LevelFourStrategicObjectiveDeletedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(LevelFourStrategicObjectiveDeletedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();

        var strategicObjectiveId = notification.StrategicObjective.Id;

        var initiatives = await context.Initiatives
            .Where(i => i.LevelFourStrategicObjectiveId == strategicObjectiveId)
            .ToListAsync(cancellationToken);

        var kpis = await context.KPIs
            .Where(k => k.LevelFourStrategicObjectiveId == strategicObjectiveId)
            .ToListAsync(cancellationToken);

        initiatives.ForEach(i => i.LevelFourStrategicObjective = null);
        kpis.ForEach(k => k.LevelFourStrategicObjective = null);

        await context.SaveChangesAsync(cancellationToken);
    }
}