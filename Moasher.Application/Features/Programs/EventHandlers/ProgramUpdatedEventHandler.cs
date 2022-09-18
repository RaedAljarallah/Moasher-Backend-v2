using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Programs;

namespace Moasher.Application.Features.Programs.EventHandlers;

public class ProgramUpdatedEventHandler : INotificationHandler<ProgramUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ProgramUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(ProgramUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();

        var programId = notification.Program.Id;
        var program = await context.Programs
            .Include(p => p.Initiatives)
            .FirstOrDefaultAsync(p => p.Id == programId, cancellationToken);
        
        program?.Initiatives.ToList().ForEach(i =>
        {
            i.Program = program;
        });
        
        await context.SaveChangesAsync(cancellationToken);
    }
}