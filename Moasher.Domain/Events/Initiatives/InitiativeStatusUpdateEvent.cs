using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Initiatives;

public class InitiativeStatusUpdateEvent : DomainEvent
{
    public Initiative Initiative { get; }

    public InitiativeStatusUpdateEvent(Initiative initiative)
    {
        Initiative = initiative;
    }
}