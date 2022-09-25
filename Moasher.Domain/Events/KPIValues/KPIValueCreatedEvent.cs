using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Events.KPIValues;

public class KPIValueCreatedEvent : DomainEvent
{
    public KPIValue Value { get; }

    public KPIValueCreatedEvent(KPIValue value)
    {
        Value = value;
    }
}