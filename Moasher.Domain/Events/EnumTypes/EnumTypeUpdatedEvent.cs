using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.EnumTypes;

public class EnumTypeUpdatedEvent : DomainEvent
{
    public EnumType EnumType { get; }

    public EnumTypeUpdatedEvent(EnumType enumType)
    {
        EnumType = enumType;
    }
}