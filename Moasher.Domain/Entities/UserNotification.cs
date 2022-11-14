using Moasher.Domain.Common.Interfaces;

namespace Moasher.Domain.Entities;

public class UserNotification : IDbEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
    public bool HasRead { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ReadAt { get; set; }
    public Guid UserId { get; set; }
}