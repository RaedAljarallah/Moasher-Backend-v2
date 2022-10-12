using Moasher.Application.Common.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Contracts;

public record ContractDto : DtoBase
{
    public string Name { get; set; } = default!;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal Amount { get; set; }
    public string? RefNumber { get; set; }
    public EnumValue Status { get; set; } = default!;
    public string? Supplier { get; set; }
    public decimal? CurrentYearExpenditure { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal PlannedExpenditureToDate { get; set; }
    public string EntityName { get; set; } = default!;
    public string InitiativeName { get; set; } = default!;
    public Guid InitiativeId { get; set; }
    public Guid ProjectId { get; set; }
    public decimal Remaining => GetRemaining();

    private decimal GetRemaining()
    {
        if (!TotalExpenditure.HasValue)
        {
            return Amount;
        }

        return Amount - TotalExpenditure.Value;
    }
}