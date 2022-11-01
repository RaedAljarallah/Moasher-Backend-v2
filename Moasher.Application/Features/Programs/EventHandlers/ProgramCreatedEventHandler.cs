using MediatR;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.Programs;

namespace Moasher.Application.Features.Programs.EventHandlers;

public class ProgramCreatedEventHandler : INotificationHandler<ProgramCreatedEvent>
{
    private readonly IMoasherDbContext _context;

    public ProgramCreatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(ProgramCreatedEvent notification, CancellationToken cancellationToken)
    {
        var program = notification.Program;
        var searchRecord = new Search(program.Id, program.Name, SearchCategory.Program);
        _context.SearchRecords.Add(searchRecord);
        await _context.SaveChangesAsync(cancellationToken);
    }
}