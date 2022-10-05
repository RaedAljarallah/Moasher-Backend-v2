using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeExpenditure : AuditableDbEntity
{
    public ushort Year { get; set; }
    public Month Month { get; set; }
    public decimal InitialPlannedAmount { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal? ActualAmount { get; set; }
    public InitiativeProject Project { get; set; } = default!;
    public Guid ProjectId { get; set; }
}