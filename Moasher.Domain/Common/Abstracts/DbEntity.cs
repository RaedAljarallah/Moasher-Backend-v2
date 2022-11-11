using Moasher.Domain.Common.Interfaces;
using Newtonsoft.Json;

namespace Moasher.Domain.Common.Abstracts;

public abstract class DbEntity : IDbEntity
{
    private readonly List<DomainEvent> _domainEvents = new();
    
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    public bool HasDomainEvents()
    {
        return _domainEvents.Any();
    }
}