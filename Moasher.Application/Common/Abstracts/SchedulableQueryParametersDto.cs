namespace Moasher.Application.Common.Abstracts;

public record SchedulableQueryParametersDto : QueryParameterBase
{
    public string? St { get; set; }
    public DateTimeOffset? Du { get; set; }
    public DateTimeOffset? Pf { get; set; }
    public DateTimeOffset? Pt { get; set; }
    public DateTimeOffset? Af { get; set; }
    public DateTimeOffset? At { get; set; }
}