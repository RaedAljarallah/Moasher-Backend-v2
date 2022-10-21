using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.Expenditures;

public record ExpenditureDto : DtoBase
{
    public int Year { get; set; }
    public Month Month { get; set; }
    public decimal InitialPlannedAmount { get; set; }
    public decimal InitialPlannedAmountCumulative { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal PlannedAmountCumulative { get; set; }
    public decimal ActualAmount { get; set; }
    public decimal ActualAmountCumulative { get; set; }
    public decimal Budget { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? ContractId { get; set; }
    public Guid? InitiativeId { get; set; }
}