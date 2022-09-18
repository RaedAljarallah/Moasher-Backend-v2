using MediatR;

namespace Moasher.Domain.Common.Abstracts;

public abstract class DomainEvent : INotification
{
    // public bool IsPublished { get; set; }
    // public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow.AddHours(3);
}