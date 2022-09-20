using System.Threading.Channels;
using MediatR;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.BackgroundJobs.Common;

public class BackgroundQueue : IBackgroundQueue
{
    private readonly Channel<Func<CancellationToken, Task<INotification>>> _queue;

    public BackgroundQueue(int capacity)
    {
        BoundedChannelOptions options = new(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue = Channel.CreateBounded<Func<CancellationToken, Task<INotification>>>(options);
    }
    
    public async Task QueueTask(Func<CancellationToken, Task<INotification>> task)
    {
        if (task is null)
        {
            throw new ArgumentNullException(nameof(task));
        }
        
        await _queue.Writer.WriteAsync(task);
    }

    public async Task<Func<CancellationToken, Task<INotification>>> DequeueAsync(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}