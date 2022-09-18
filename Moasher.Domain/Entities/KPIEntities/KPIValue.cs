using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities.KPIEntities;

public class KPIValue : AuditableDbEntity, ISchedulable
{
    private KPI _kpi = default!;
    public TimePeriod MeasurementPeriod { get; set; }
    public short Year { get; set; }
    public float TargetValue { get; set; }
    public float? ActualValue { get; set; }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public Polarity Polarity { get; private set; } = default!;
    public string EntityName { get; private set; } = default!;
    public string KPIName { get; private set; } = default!;

    public KPI KPI
    {
        get => _kpi;
        set
        {
            _kpi = value;
            KPIName = value.Name;
            EntityName = value.EntityName;
            Polarity = value.Polarity;
        }
    }
    public Guid KPIId { get; set; }
}