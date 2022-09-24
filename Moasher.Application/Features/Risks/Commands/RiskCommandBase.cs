namespace Moasher.Application.Features.Risks.Commands;

public abstract record RiskCommandBase
{
    private string _description = default!;
    private string _impactDescription = default!;
    private string? _owner;
    private string _responsePlane = default!;
    private string _raisedBy = default!;

    public string Description { get => _description; set => _description = value.Trim(); }
    public string ImpactDescription { get => _impactDescription; set => _impactDescription = value.Trim(); }
    public string? Owner { get => _owner; set => _owner = value?.Trim(); }
    public string ResponsePlane { get => _responsePlane; set => _responsePlane = value.Trim(); }
    public string RaisedBy { get => _raisedBy; set => _raisedBy = value.Trim(); }
    public DateTimeOffset RaisedAt { get; set; }
    public Guid TypeEnumId { get; set; }
    public Guid PriorityEnumId { get; set; }
    public Guid ProbabilityEnumId { get; set; }
    public Guid ImpactEnumId { get; set; }
    public Guid ScopeEnumId { get; set; }
    public Guid InitiativeId { get; set; }
}