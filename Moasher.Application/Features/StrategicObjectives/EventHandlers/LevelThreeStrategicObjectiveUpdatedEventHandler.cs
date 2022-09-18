using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.StrategicObjectives;

namespace Moasher.Application.Features.StrategicObjectives.EventHandlers;

public class LevelThreeStrategicObjectiveUpdatedEventHandler : INotificationHandler<LevelThreeStrategicObjectiveUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LevelThreeStrategicObjectiveUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(LevelThreeStrategicObjectiveUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();

        var strategicObjectiveId = notification.StrategicObjective.Id;
        
        var strategicObjective = await context.StrategicObjectives
            .Include(o => o.Initiatives)
            .Include(o => o.KPIs)
            .FirstOrDefaultAsync(o => o.Id == strategicObjectiveId, cancellationToken);

        if (strategicObjective is not null)
        {
            strategicObjective.Initiatives.ToList().ForEach(i => i.LevelThreeStrategicObjective = strategicObjective);
            strategicObjective.KPIs.ToList().ForEach(k => k.LevelThreeStrategicObjective = strategicObjective);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}