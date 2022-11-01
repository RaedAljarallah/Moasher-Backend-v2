using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.KPIs;

namespace Moasher.Application.Features.KPIs.EventHandlers;

public class KPIUpdatedEventHandler : INotificationHandler<KPIUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public KPIUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(KPIUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var kpiId = notification.Kpi.Id;
        
        var kpi = await _context.KPIs
            .Include(k => k.Values)
            .Include(k => k.Analytics)
            .AsSplitQuery()
            .FirstOrDefaultAsync(k => k.Id == kpiId, cancellationToken);

        if (kpi is not null)
        {
            kpi.Values.ToList().ForEach(v => v.KPI = kpi);
            kpi.Analytics.ToList().ForEach(a => a.KPI = kpi);
            
            _context.KPIValues.UpdateRange(kpi.Values);
            _context.Analytics.UpdateRange(kpi.Analytics);
            
            var searchRecord = await _context.SearchRecords.FirstOrDefaultAsync(s => s.RelativeId == kpiId, cancellationToken);
            if (searchRecord is not null)
            {
                searchRecord.Title = kpi.Name;
            }
            
            await _context.SaveChangesAsync(cancellationToken);
        }
        
    }
}