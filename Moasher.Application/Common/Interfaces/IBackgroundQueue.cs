using MediatR;

namespace Moasher.Application.Common.Interfaces;

public interface IBackgroundQueue
{
    Task QueueTask(Func<CancellationToken, Task<INotification>> task);
    Task<Func<CancellationToken, Task<INotification>>> DequeueAsync(CancellationToken cancellationToken);
}