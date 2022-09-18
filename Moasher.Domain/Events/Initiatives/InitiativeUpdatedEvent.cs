using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Initiatives;

public class InitiativeUpdatedEvent : DomainEvent
{
    public Initiative Initiative { get; }

    public InitiativeUpdatedEvent(Initiative initiative)
    {
        Initiative = initiative;
    }
}