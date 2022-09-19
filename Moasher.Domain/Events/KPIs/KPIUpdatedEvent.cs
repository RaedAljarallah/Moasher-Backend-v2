using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Events.KPIs;

public class KPIUpdatedEvent : DomainEvent
{
    public KPI Kpi { get; }

    public KPIUpdatedEvent(KPI kpi)
    {
        Kpi = kpi;
    }
}