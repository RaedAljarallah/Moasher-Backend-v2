using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Initiatives;

public class InitiativeDeletedEvent : DomainEvent
{
    public Initiative Initiative { get; }

    public InitiativeDeletedEvent(Initiative initiative)
    {
        Initiative = initiative;
    }
}