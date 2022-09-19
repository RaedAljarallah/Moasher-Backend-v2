using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.KPIs;
using Moasher.Domain.Extensions;

namespace Moasher.Application.Features.KPIs.EventHandlers;

public class KPIStatusUpdateEventHandler : INotificationHandler<KPIStatusUpdateEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public KPIStatusUpdateEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(KPIStatusUpdateEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        var kpiId = notification.Kpi.Id;
        
        var kpi = await context.KPIs
            .Include(k => k.Values)
            .FirstOrDefaultAsync(k => k.Id == kpiId, cancellationToken);

        if (kpi is not null)
        {
            if (kpi.Values.Any())
            {
                var statusEnums = await context.EnumTypes
                    .Where(e => e.Category == EnumTypeCategory.KPIStatus.ToString())
                    .ToListAsync(cancellationToken);
            
                kpi.SetStatus(statusEnums);
            }
            else
            {
                kpi.StatusEnum = null;
            }
            
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}