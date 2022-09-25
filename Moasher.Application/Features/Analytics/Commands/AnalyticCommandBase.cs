namespace Moasher.Application.Features.Analytics.Commands;

public abstract record AnalyticCommandBase
{
    private string _description = default!;
    private string _analyzedBy = default!;

    public string Description { get => _description; set => _description = value.Trim(); }
    public string AnalyzedBy { get => _analyzedBy; set => _analyzedBy = value.Trim(); }
    public DateTimeOffset AnalyzedAt { get; set; }
    public Guid? InitiativeId { get; set; }
    public Guid? KPIId { get; set; }
}