using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Programs;

namespace Moasher.Application.Features.Programs.EventHandlers;

public class ProgramDeletedEventHandler : INotificationHandler<ProgramDeletedEvent>
{
    private readonly IMoasherDbContext _context;

    public ProgramDeletedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ProgramDeletedEvent notification, CancellationToken cancellationToken)
    {
        var programId = notification.Program.Id;
        var searchRecord =
            await _context.SearchRecords.FirstOrDefaultAsync(s => s.RelativeId == programId, cancellationToken);
        if (searchRecord is not null)
        {
            _context.SearchRecords.Remove(searchRecord);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}