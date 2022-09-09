using Moasher.Application.Common.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Initiatives;

public record InitiativeDto : DtoBase
{
    public string UnifiedCode { get; set; } = default!;
    public string? CodeByProgram { get; set; }
    public string Name { get; set; } = default!;
    public string? Scope { get; set; }
    public string? TargetSegment { get; set; }
    public string? ContributionOnStrategicObjective { get; set; }
    public EnumValue? Status { get; set; }
    public EnumValue FundStatus { get; set; } = default!;
    public DateTimeOffset PlannedStart { get; set; }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualStart { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public decimal RequiredCost { get; set; }
    public string? CapexCode { get; set; }
    public string? OpexCode { get; set; }
    public string EntityName { get; set; } = default!;
    public string? PortfolioName { get; set; }
    public string ProgramName { get; set; } = default!;
    public string LevelOneStrategicObjectiveName { get; set; } = default!;
    public string LevelTwoStrategicObjectiveName { get; set; } = default!;
    public string LevelThreeStrategicObjectiveName { get; set; } = default!;
    public string? LevelFourStrategicObjectiveName { get; set; }
    public float PlannedProgress { get; set; }
    public float ActualProgress { get; set; }
    public decimal ApprovedCost { get; set; }
    public decimal TotalBudget { get; set; }
    public decimal CurrentYearBudget { get; set; }
    public decimal ContractsAmount { get; set; }
    public decimal TotalExpenditure { get; set; }
    public decimal CurrentYearExpenditure { get; set; }
    public string? LatestAnalytics { get; set; }
}