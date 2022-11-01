using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Initiatives;

public class InitiativeCreatedEvent : DomainEvent
{
    public Initiative Initiative { get; }

    public InitiativeCreatedEvent(Initiative initiative)
    {
        Initiative = initiative;
    }
}