using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Events.KPIs;

public class KPIStatusUpdateEvent : DomainEvent
{
    public KPI Kpi { get; }

    public KPIStatusUpdateEvent(KPI kpi)
    {
        Kpi = kpi;
    }
}