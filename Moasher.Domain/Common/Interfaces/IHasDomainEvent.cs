using Moasher.Domain.Common.Abstracts;

namespace Moasher.Domain.Common.Interfaces;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}