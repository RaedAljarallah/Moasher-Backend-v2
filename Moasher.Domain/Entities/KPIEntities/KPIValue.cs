using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Enums;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.KPIEntities;

[Display(Name = "مستهدفات")]
public class KPIValue : ApprovableDbEntity, ISchedulable
{
    private KPI _kpi = default!;
    public TimePeriod MeasurementPeriod { get; set; }
    public short Year { get; set; }
    public float TargetValue { get; set; }
    public float? ActualValue { get; set; }
    public string MeasurementUnit { get; set; } = default!;
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public Polarity Polarity { get; private set; }
    public string EntityName { get; private set; } = default!;
    public string KPIName { get; private set; } = default!;

    [JsonIgnore]
    public KPI KPI
    {
        get => _kpi;
        set
        {
            _kpi = value;
            KPIName = value.Name;
            EntityName = value.EntityName;
            Polarity = value.Polarity;
            MeasurementUnit = value.MeasurementUnit;
        }
    }
    public Guid KPIId { get; set; }
}