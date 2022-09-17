namespace Moasher.Application.Common.Interfaces;

public interface IBackgroundQueue
{
    Task QueueTask(Func<CancellationToken, Task> task);
    Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
}