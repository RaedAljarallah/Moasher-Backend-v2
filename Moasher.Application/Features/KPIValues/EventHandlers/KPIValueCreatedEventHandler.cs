using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.KPIValues;
using Moasher.Domain.Extensions;

namespace Moasher.Application.Features.KPIValues.EventHandlers;

public class KPIValueCreatedEventHandler : INotificationHandler<KPIValueCreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public KPIValueCreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(KPIValueCreatedEvent notification, CancellationToken cancellationToken)
    {
        var kpiId = notification.Value.KPIId;
        var kpi = await _context.KPIs.Include(k => k.Values)
            .FirstOrDefaultAsync(k => k.Id == kpiId, cancellationToken);

        if (kpi is not null)
        {
            kpi.SetProgress();
            if (kpi.CalculateStatus)
            {
                var status = await _context.EnumTypes
                    .Where(e => e.Category.ToLower() == EnumTypeCategory.KPIStatus.ToString().ToLower())
                    .ToListAsync(cancellationToken);
                
                kpi.SetStatus(status);
            }

            _context.KPIs.Update(kpi);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}