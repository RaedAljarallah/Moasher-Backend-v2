using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.KPIValues;

namespace Moasher.Application.Features.KPIValues.EventHandlers;

public class KPIValueUpdatedEventHandler : INotificationHandler<KPIValueUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public KPIValueUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(KPIValueUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var kpiId = notification.Value.KPIId;
        var kpi = await _context.KPIs.Include(k => k.Values)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(k => k.Id == kpiId, cancellationToken);

        if (kpi is not null)
        {
            kpi.SetProgress();
            if (kpi.CalculateStatus)
            {
                var status = await _context.EnumTypes
                    .IgnoreQueryFilters()
                    .Where(e => e.Category.ToLower() == EnumTypeCategory.KPIStatus.ToString().ToLower())
                    .ToListAsync(cancellationToken);
                
                kpi.SetStatus(status);
            }

            _context.KPIs.Update(kpi);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}