using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Events.KPIValues;

public class KPIValueDeletedEvent : DomainEvent
{
    public KPIValue Value { get; }

    public KPIValueDeletedEvent(KPIValue value)
    {
        Value = value;
    }
}