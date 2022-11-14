namespace Moasher.Application.Features.UserNotifications;

public record UserNotificationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
    public bool HasRead { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ReadAt { get; set; }
}