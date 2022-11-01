using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.Entities;

public class EntityCreatedEvent : DomainEvent
{
    public Entity Entity { get; }

    public EntityCreatedEvent(Entity entity)
    {
        Entity = entity;
    }
}