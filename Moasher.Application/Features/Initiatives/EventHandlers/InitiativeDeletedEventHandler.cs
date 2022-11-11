using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Initiatives;

namespace Moasher.Application.Features.Initiatives.EventHandlers;

public class InitiativeDeletedEventHandler : INotificationHandler<InitiativeDeletedEvent>
{
    private readonly IMoasherDbContext _context;

    public InitiativeDeletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(InitiativeDeletedEvent notification, CancellationToken cancellationToken)
    {
        var initiativeId = notification.Initiative.Id;
        var searchRecord =
            await _context.SearchRecords.FirstOrDefaultAsync(s => s.RelativeId == initiativeId, cancellationToken);
        if (searchRecord is not null)
        {
            _context.SearchRecords.Remove(searchRecord);
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}