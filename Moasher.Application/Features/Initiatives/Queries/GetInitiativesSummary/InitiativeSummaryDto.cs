using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesSummary;

public record InitiativeSummaryDto
{
    public List<EnumValue> Statuses { get; set; } = new();
    public List<EnumValue> FundStatuses { get; set; } = new();
    public float PlannedProgress { get; set; }
    public float ActualProgress { get; set; }
    public decimal RequiredCost { get; set; }
    public decimal ApprovedCost { get; set; }
    public decimal CurrentYearBudget { get; set; }
    public decimal TotalBudget { get; set; }
    public decimal ContractsAmount { get; set; }
    public decimal TotalExpenditure { get; set; }
    public decimal CurrentYearExpenditure { get; set; }
    public decimal EstimatedBudgetAtCompletion { get; set; }
    public decimal PlannedToDateExpenditure { get; set; }
    public decimal PlannedToDateContractsAmount { get; set; }
}