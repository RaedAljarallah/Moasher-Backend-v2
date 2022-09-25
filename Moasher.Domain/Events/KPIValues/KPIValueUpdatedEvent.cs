using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Events.KPIValues;

public class KPIValueUpdatedEvent : DomainEvent
{
    public KPIValue Value { get; }

    public KPIValueUpdatedEvent(KPIValue value)
    {
        Value = value;
    }
}