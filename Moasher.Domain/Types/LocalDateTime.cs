namespace Moasher.Domain.Types;

public readonly struct LocalDateTime
{
    public static readonly DateTimeOffset Now = DateTimeOffset.UtcNow.AddHours(3);
}