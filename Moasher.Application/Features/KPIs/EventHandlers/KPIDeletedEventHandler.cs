using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.KPIs;

namespace Moasher.Application.Features.KPIs.EventHandlers;

public class KPIDeletedEventHandler : INotificationHandler<KPIDeletedEvent>
{
    private readonly IMoasherDbContext _context;

    public KPIDeletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(KPIDeletedEvent notification, CancellationToken cancellationToken)
    {
        var kpiId = notification.Kpi.Id;
        var searchRecord =
            await _context.SearchRecords
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.RelativeId == kpiId, cancellationToken);
        if (searchRecord is not null)
        {
            _context.SearchRecords.Remove(searchRecord);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}