using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Events.KPIs;

public class KPIDeletedEvent : DomainEvent
{
    public KPI Kpi { get; }

    public KPIDeletedEvent(KPI kpi)
    {
        Kpi = kpi;
    }
}