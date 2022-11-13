using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class Initiative : AuditableDbEntity, IRootEntity
{
    private EnumType? _statusEnum;
    private EnumType _fundStatusEnum = default!;
    private Entity _entity = default!;
    private Portfolio? _portfolio;
    private Program _program = default!;
    private StrategicObjective _levelOneStrategicObjective = default!;
    private StrategicObjective _levelTwoStrategicObjective = default!;
    private StrategicObjective _levelThreeStrategicObjective = default!;
    private StrategicObjective? _levelFourStrategicObjective;
    private Analytic? _latestAnalyticsModel;
    
    public string UnifiedCode { get; set; } = default!;
    public string? CodeByProgram { get; set; }
    public string Name { get; set; } = default!;
    public string? Scope { get; set; }
    public string? TargetSegment { get; set; }
    public string? ContributionOnStrategicObjective { get; set; }

    public string? StatusName { get; private set; }
    public string? StatusStyle { get; private set; }
    public EnumType? StatusEnum
    {
        get => _statusEnum;
        set
        {
            _statusEnum = value;
            StatusName = value?.Name;
            StatusStyle = value?.Style;
        }
    }
    public Guid? StatusEnumId { get; set; }

    public string FundStatusName { get; private set; } = default!;
    public string FundStatusStyle { get; private set; } = default!;

    public EnumType FundStatusEnum
    {
        get => _fundStatusEnum;
        set
        {
            _fundStatusEnum = value;
            FundStatusName = value.Name;
            FundStatusStyle = value.Style;
        }
    }

    public Guid? FundStatusEnumId { get; set; }
    public DateTimeOffset PlannedStart { get; set; }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualStart { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public decimal RequiredCost { get; set; }
    public string? CapexCode { get; set; }
    public string? OpexCode { get; set; }
    public bool Visible { get; set; }
    public bool VisibleOnDashboard { get; set; }
    public bool CalculateStatus { get; set; }
    public string EntityName { get; private set; } = default!;

    public Entity Entity
    {
        get => _entity;
        set
        {
            _entity = value;
            EntityName = value.Name;
        }
    }

    public Guid EntityId { get; set; }
    public string? PortfolioName { get; private set; }

    public Portfolio? Portfolio
    {
        get => _portfolio;
        set
        {
            _portfolio = value;
            PortfolioName = value?.Name;
        }
    }

    public Guid? PortfolioId { get; set; }
    public string ProgramName { get; private set; } = default!;

    public Program Program
    {
        get => _program;
        set
        {
            _program = value;
            ProgramName = value.Name;
        }
    }

    public Guid ProgramId { get; set; }
    public string LevelOneStrategicObjectiveName { get; private set; } = default!;

    public StrategicObjective LevelOneStrategicObjective
    {
        get => _levelOneStrategicObjective;
        set
        {
            _levelOneStrategicObjective = value;
            LevelOneStrategicObjectiveName = value.Name;
            LevelOneStrategicObjectiveId = value.Id;
        }
    }

    public Guid LevelOneStrategicObjectiveId { get; private set; }

    public string LevelTwoStrategicObjectiveName { get; private set; } = default!;

    public StrategicObjective LevelTwoStrategicObjective
    {
        get => _levelTwoStrategicObjective;
        set
        {
            _levelTwoStrategicObjective = value;
            LevelTwoStrategicObjectiveName = value.Name;
            LevelTwoStrategicObjectiveId = value.Id;
        }
    }

    public Guid LevelTwoStrategicObjectiveId { get; private set; }

    public string LevelThreeStrategicObjectiveName { get; private set; } = default!;

    public StrategicObjective LevelThreeStrategicObjective
    {
        get => _levelThreeStrategicObjective;
        set
        {
            _levelThreeStrategicObjective = value;
            LevelThreeStrategicObjectiveName = value.Name;
        }
    }

    public Guid LevelThreeStrategicObjectiveId { get; set; }

    public string? LevelFourStrategicObjectiveName { get; private set; }

    public StrategicObjective? LevelFourStrategicObjective
    {
        get => _levelFourStrategicObjective;
        set
        {
            _levelFourStrategicObjective = value;
            LevelFourStrategicObjectiveName = value?.Name;
            LevelFourStrategicObjectiveId = value?.Id;
        }
    }

    public Guid? LevelFourStrategicObjectiveId { get; private set; }
    public float? PlannedProgress { get; set; }
    public float? ActualProgress { get; set; }
    
    public ICollection<InitiativeMilestone> Milestones { get; set; }
        = new HashSet<InitiativeMilestone>();
    
    public ICollection<InitiativeDeliverable> Deliverables { get; set; }
        = new HashSet<InitiativeDeliverable>();
    
    public decimal? ApprovedCost { get; set; }
    public ICollection<InitiativeApprovedCost> ApprovedCosts { get; set; }
        = new HashSet<InitiativeApprovedCost>();
    
    public decimal? CurrentYearBudget { get; set; }
    public decimal? TotalBudget { get; set; }
    public ICollection<InitiativeBudget> Budgets { get; set; }
        = new HashSet<InitiativeBudget>();
    
    
    public ICollection<InitiativeImpact> Impacts { get; set; }
        = new HashSet<InitiativeImpact>();
    
    public ICollection<InitiativeTeam> Teams { get; set; }
        = new HashSet<InitiativeTeam>();
    
    public ICollection<InitiativeIssue> Issues { get; set; }
        = new HashSet<InitiativeIssue>();

    public ICollection<InitiativeRisk> Risks { get; set; }
        = new HashSet<InitiativeRisk>();
    
    public decimal? ContractsAmount { get; set; }
    public ICollection<InitiativeContract> Contracts { get; set; }
        = new HashSet<InitiativeContract>();
    public decimal? TotalExpenditure { get; set; }
    public decimal? CurrentYearExpenditure { get; set; }
    public string? LatestAnalytics { get; private set; }

    public ICollection<InitiativeProject> Projects { get; set; }
        = new HashSet<InitiativeProject>();
    
    public DateTimeOffset? LatestAnalyticsDate { get; private set; }
    public Analytic? LatestAnalyticsModel
    {
        get => _latestAnalyticsModel;
        set
        {
            _latestAnalyticsModel = value;
            LatestAnalytics = value?.Description;
            LatestAnalyticsDate = value?.AnalyzedAt;
        }
    }
    public ICollection<Analytic> Analytics { get; set; }
        = new HashSet<Analytic>();
}