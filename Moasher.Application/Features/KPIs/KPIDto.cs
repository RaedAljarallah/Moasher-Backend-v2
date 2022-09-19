using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.KPIs;

public record KPIDto : DtoBase
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string OwnerName { get; set; } = default!;
    public string OwnerEmail { get; set; } = default!;
    public string OwnerPhoneNumber { get; set; } = default!;
    public string? OwnerPosition { get; set; }
    public Frequency Frequency { get; set; }
    public Polarity Polarity { get; set; }
    public ValidationStatus ValidationStatus { get; set; }
    public string? Formula { get; set; }
    public float? BaselineValue { get; set; }
    public short? BaselineYear { get; set; }
    public string MeasurementUnit { get; set; } = default!;
    public string? DataSource { get; set; }
    public string Description { get; set; } = default!;
    public float? PlannedProgress { get; set; }
    public float? ActualProgress { get; set; }
    public EnumValue? Status { get; set; }
    public string EntityName { get; set; } = default!;
    public string LevelOneStrategicObjectiveName { get; set; } = default!;
    public string LevelTwoStrategicObjectiveName { get; set; } = default!;
    public string LevelThreeStrategicObjectiveName { get; set; } = default!;
    public string? LevelFourStrategicObjectiveName { get; set; }
}