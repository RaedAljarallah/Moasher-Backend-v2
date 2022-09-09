namespace Moasher.Domain.Common.Abstracts;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}
public abstract class DomainEvent
{
    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow.AddHours(3);
}