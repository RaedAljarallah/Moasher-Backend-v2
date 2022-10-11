using Moasher.Domain.Enums;

namespace Moasher.Application.Features.Expenditures.Commands;

public abstract record ExpenditureCommandBase
{
    public ushort Year { get; set; }
    public Month Month { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal? ActualAmount { get; set; }
    public Guid ProjectId { get; set; }
}