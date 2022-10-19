using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.Projects.Queries.GetProjectsSummary;

public record ProjectsSummaryDto : DtoBase
{
    public int Year { get; set; }
    public Month Month { get; set; }
    public decimal InitialPlannedAmount { get; set; }
    public decimal InitialPlannedAmountCumulative { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal PlannedAmountCumulative { get; set; }
    public decimal ActualAmount { get; set; }
    public decimal ActualAmountCumulative { get; set; }
    public decimal ApprovedCost { get; set; }
    public Guid? InitiativeId { get; set; }
}