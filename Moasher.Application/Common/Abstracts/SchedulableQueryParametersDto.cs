namespace Moasher.Application.Common.Abstracts;

public record SchedulableQueryParametersDto : QueryParameterBase
{
    private string? _status;

    public string? Status
    {
        get => _status;
        set => _status = value?.Trim();
    }

    public DateTimeOffset? DueUntil { get; set; }
    public DateTimeOffset? PlannedFrom { get; set; }
    public DateTimeOffset? PlannedTo { get; set; }
    public DateTimeOffset? ActualFrom { get; set; }
    public DateTimeOffset? ActualTo { get; set; }
}