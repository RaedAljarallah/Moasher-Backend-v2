using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Interfaces;

public interface IUserNotification
{
    public Task<UserNotification> CreateAsync(string title, string body, User user, CancellationToken cancellationToken = new());
    public Task<UserNotification> CreateAsync(string title, string body, IEnumerable<User> users, CancellationToken cancellationToken = new());
    public Task NotifyAsync(UserNotification notification, User user, CancellationToken cancellationToken = new());
    public Task NotifyAsync(UserNotification notification, IEnumerable<User> users, CancellationToken cancellationToken = new());
}