using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Programs;

namespace Moasher.Application.Features.Programs.EventHandlers;

public class ProgramUpdatedEventHandler : INotificationHandler<ProgramUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public ProgramUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ProgramUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var programId = notification.Program.Id;
        var program = await _context.Programs
            .IgnoreQueryFilters()
            .Include(p => p.Initiatives)
            .FirstOrDefaultAsync(p => p.Id == programId, cancellationToken);

        if (program is not null)
        {
            program.Initiatives.ToList().ForEach(i =>
            {
                i.Program = program;
            });
            
            _context.Initiatives.UpdateRange(program.Initiatives);
            
            var searchRecord = await _context.SearchRecords
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.RelativeId == programId, cancellationToken);
            if (searchRecord is not null)
            {
                searchRecord.Title = program.Name;
            }
            await _context.SaveChangesAsyncFromDomainEvent(cancellationToken);
        }
    }
}