using MediatR;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Initiatives;

namespace Moasher.Application.Features.Initiatives.EventHandlers;

public class InitiativeCreatedEventHandler : INotificationHandler<InitiativeCreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public InitiativeCreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(InitiativeCreatedEvent notification, CancellationToken cancellationToken)
    {
        var initiative = notification.Initiative;
        var searchRecord = new Search(initiative.Id, initiative.Name, SearchCategory.Initiative);
        _context.SearchRecords.Add(searchRecord);
        await _context.SaveChangesAsync(cancellationToken);
    }
}