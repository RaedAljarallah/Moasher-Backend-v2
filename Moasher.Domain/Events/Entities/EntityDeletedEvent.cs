using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.Entities;

public class EntityDeletedEvent : DomainEvent
{
    public Entity Entity { get; }

    public EntityDeletedEvent(Entity entity)
    {
        Entity = entity;
    }
}