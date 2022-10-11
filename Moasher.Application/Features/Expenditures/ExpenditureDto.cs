using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.Expenditures;

public record ExpenditureDto : DtoBase
{
    public ushort Year { get; set; }
    public Month Month { get; set; }
    public decimal InitialPlannedAmount { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal? ActualAmount { get; set; }
    public Guid ProjectId { get; set; }
}