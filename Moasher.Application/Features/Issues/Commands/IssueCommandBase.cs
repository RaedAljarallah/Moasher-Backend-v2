namespace Moasher.Application.Features.Issues.Commands;

public abstract record IssueCommandBase
{
    private string _description = default!;
    private string _impactDescription = default!;
    private string _source = default!;
    private string _reason = default!;
    private string _resolution = default!;
    private string _raisedBy = default!;

    public string Description { get => _description; set => _description = value.Trim(); }
    public string ImpactDescription { get => _impactDescription; set => _impactDescription = value.Trim(); }
    public string Source { get => _source; set => _source = value.Trim(); }
    public string Reason { get => _reason; set => _reason = value.Trim(); }
    public string Resolution { get => _resolution; set => _resolution = value.Trim(); }
    public string RaisedBy { get => _raisedBy; set => _raisedBy = value.Trim(); }
    public DateTimeOffset EstimatedResolutionDate { get; set; }
    public DateTimeOffset RaisedAt { get; set; }
    public DateTimeOffset? ClosedAt { get; set; }
    public Guid ScopeEnumId { get; set; }
    public Guid StatusEnumId { get; set; }
    public Guid ImpactEnumId { get; set; }
    public Guid InitiativeId { get; set; }
}