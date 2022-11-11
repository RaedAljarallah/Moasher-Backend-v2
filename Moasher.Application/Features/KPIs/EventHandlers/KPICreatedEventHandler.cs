using MediatR;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.KPIs;

namespace Moasher.Application.Features.KPIs.EventHandlers;

public class KPICreatedEventHandler : INotificationHandler<KPICreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public KPICreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(KPICreatedEvent notification, CancellationToken cancellationToken)
    {
        var kpi = notification.Kpi;
        var searchRecord = new Search(kpi.Id, kpi.Name, SearchCategory.KPI);
        _context.SearchRecords.Add(searchRecord);
        await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
    }
}