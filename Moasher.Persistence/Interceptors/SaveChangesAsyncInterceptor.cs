using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Abstracts;
using Moasher.Persistence.Extensions;

namespace Moasher.Persistence.Interceptors;

public class SaveChangesAsyncInterceptor : SaveChangesInterceptor
{
    private readonly IBackgroundQueue _backgroundQueue;

    public SaveChangesAsyncInterceptor(IBackgroundQueue backgroundQueue)
    {
        _backgroundQueue = backgroundQueue;
    }
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        var events = eventData.Context.GetDomainEvents();
        eventData.Context.UpdateAuditRecords();
        var savingResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
        await DispatchEvents(events);
        return savingResult;
    }
    
    private async Task DispatchEvents(List<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _backgroundQueue.QueueTask(ct => Task.Factory.StartNew(() => domainEvent as INotification, ct));
        }
    }
}