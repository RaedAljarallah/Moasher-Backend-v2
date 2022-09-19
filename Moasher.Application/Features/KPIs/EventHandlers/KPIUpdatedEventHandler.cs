using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.KPIs;

namespace Moasher.Application.Features.KPIs.EventHandlers;

public class KPIUpdatedEventHandler : INotificationHandler<KPIUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public KPIUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(KPIUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        var kpiId = notification.Kpi.Id;
        
        var kpi = await context.KPIs
            .Include(k => k.Values)
            .Include(k => k.Analytics)
            .AsSplitQuery()
            .FirstOrDefaultAsync(k => k.Id == kpiId, cancellationToken);

        if (kpi is not null)
        {
            kpi.Values.ToList().ForEach(v => v.KPI = kpi);
            kpi.Analytics.ToList().ForEach(a => a.KPI = kpi);
            await context.SaveChangesAsync(cancellationToken);
        }
        
    }
}