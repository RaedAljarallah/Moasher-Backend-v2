using MediatR;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Entities;

namespace Moasher.Application.Features.Entities.EventHandlers;

public class EntityCreatedEventHandler : INotificationHandler<EntityCreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public EntityCreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(EntityCreatedEvent notification, CancellationToken cancellationToken)
    {
        var entity = notification.Entity;
        var searchRecord = new Search(entity.Id, entity.Name, SearchCategory.Entity);
        _context.SearchRecords.Add(searchRecord);
        await _context.SaveChangesAsync(cancellationToken);
    }
}