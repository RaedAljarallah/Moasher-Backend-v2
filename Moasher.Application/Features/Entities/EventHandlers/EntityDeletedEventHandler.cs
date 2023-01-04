using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Entities;

namespace Moasher.Application.Features.Entities.EventHandlers;

public class EntityDeletedEventHandler : INotificationHandler<EntityDeletedEvent>
{
    private readonly IMoasherDbContext _context;

    public EntityDeletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(EntityDeletedEvent notification, CancellationToken cancellationToken)
    {
        var entityId = notification.Entity.Id;
        var searchRecord =
            await _context.SearchRecords
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.RelativeId == entityId, cancellationToken);
        if (searchRecord is not null)
        {
            _context.SearchRecords.Remove(searchRecord);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}