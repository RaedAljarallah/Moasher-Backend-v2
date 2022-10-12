using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.KPIs;

namespace Moasher.Application.Features.KPIs.EventHandlers;

public class KPIStatusUpdateEventHandler : INotificationHandler<KPIStatusUpdateEvent>
{
    private readonly IMoasherDbContext _context;

    public KPIStatusUpdateEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(KPIStatusUpdateEvent notification, CancellationToken cancellationToken)
    {
        var kpiId = notification.Kpi.Id;
        
        var kpi = await _context.KPIs
            .Include(k => k.Values)
            .FirstOrDefaultAsync(k => k.Id == kpiId, cancellationToken);

        if (kpi is not null)
        {
            if (kpi.Values.Any())
            {
                var statusEnums = await _context.EnumTypes
                    .Where(e => e.Category == EnumTypeCategory.KPIStatus.ToString())
                    .ToListAsync(cancellationToken);
            
                kpi.SetStatus(statusEnums);
            }
            else
            {
                kpi.StatusEnum = null;
            }

            _context.KPIs.Update(kpi);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}