using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Events.KPIs;

public class KPICreatedEvent : DomainEvent
{
    public KPI Kpi { get; }

    public KPICreatedEvent(KPI kpi)
    {
        Kpi = kpi;
    }
}