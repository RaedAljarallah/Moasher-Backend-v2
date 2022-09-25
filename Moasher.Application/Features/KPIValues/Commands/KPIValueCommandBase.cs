using Moasher.Domain.Enums;

namespace Moasher.Application.Features.KPIValues.Commands;

public abstract record KPIValueCommandBase
{
    public TimePeriod MeasurementPeriod { get; set; }
    public short Year { get; set; }
    public float TargetValue { get; set; }
    public float? ActualValue { get; set; }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public Guid KPIId { get; set; }
}