namespace Moasher.Application.Common.Abstracts;

public record SchedulableQueryParametersDto : QueryParameterBase
{
    public string? St { get; set; }
    public DateTimeOffset? DueUntil { get; set; }
    public DateTimeOffset? PlannedFrom { get; set; }
    public DateTimeOffset? PlannedTo { get; set; }
    public DateTimeOffset? ActualFrom { get; set; }
    public DateTimeOffset? ActualTo { get; set; }
}