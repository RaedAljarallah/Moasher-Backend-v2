using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.KPIValues;

public record KPIValueDto : DtoBase, ISchedulableSummary
{
    public TimePeriod MeasurementPeriod { get; set; }
    public short Year { get; set; }
    public float TargetValue { get; set; }
    public float? ActualValue { get; set; }
    public string MeasurementUnit { get; set; } = default!;
    public Polarity Polarity { get; set; }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public string KPIName { get; set; } = default!;
    public Guid KPIId { get; set; } = default!;
    public string? Status => SchedulableEntityService.GetStatus(PlannedFinish, ActualFinish);
}