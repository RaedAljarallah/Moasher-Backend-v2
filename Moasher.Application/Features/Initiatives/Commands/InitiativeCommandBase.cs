namespace Moasher.Application.Features.Initiatives.Commands;

public abstract record InitiativeCommandBase
{
    private string _name = default!;
    private string _unifiedCode = default!;
    private string? _codeByProgram;
    private string? _scope;
    private string? _targetSegment;
    private string? _contributionOnStrategicObjective;
    private string? _capexCode;
    private string? _opexCode;

    public string Name { get => _name; set => _name = value.Trim(); }
    public string UnifiedCode { get => _unifiedCode; set => _unifiedCode = value.Trim(); }
    public string? CodeByProgram { get => _codeByProgram; set => _codeByProgram = value?.Trim(); }
    public string? Scope { get => _scope; set => _scope = value?.Trim(); }
    public string? TargetSegment { get => _targetSegment; set => _targetSegment = value?.Trim(); }
    public string? ContributionOnStrategicObjective 
    { 
        get => _contributionOnStrategicObjective; 
        set => _contributionOnStrategicObjective = value?.Trim(); 
    }
    public DateTimeOffset PlannedStart { get; set; }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualStart { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public decimal RequiredCost { get; set; }
    public string? CapexCode { get => _capexCode; set => _capexCode = value?.Trim(); }
    public string? OpexCode { get => _opexCode; set => _opexCode = value?.Trim(); }
    public bool Visible { get; set; }
    public bool VisibleOnDashboard { get; set; }
    public bool CalculateStatus { get; set; }
    public Guid? StatusEnumId { get; set; }
    public Guid FundStatusEnumId { get; set; }
    public Guid EntityId { get; set; }
    public Guid ProgramId { get; set; }
    public Guid? PortfolioId { get; set; }
    public Guid LevelThreeStrategicObjectiveId { get; set; }
    public Guid? LevelFourStrategicObjectiveId { get; set; }
}