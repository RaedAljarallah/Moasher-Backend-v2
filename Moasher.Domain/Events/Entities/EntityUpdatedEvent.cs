using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.Entities;

public class EntityUpdatedEvent : DomainEvent
{
    public Entity Entity { get; }

    public EntityUpdatedEvent(Entity entity)
    {
        Entity = entity;
    }
}