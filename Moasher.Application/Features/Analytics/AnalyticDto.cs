using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Analytics;

public record AnalyticDto : DtoBase
{
    public string Description { get; set; } = default!;
    public DateTimeOffset AnalyzedAt { get; set; }
    public string AnalyzedBy { get; set; } = default!;
    public string? InitiativeName { get; set; }
    public Guid? InitiativeId { get; set; }
    public string? KPIName { get; set; }
    public Guid? KPIId { get; set; }
}