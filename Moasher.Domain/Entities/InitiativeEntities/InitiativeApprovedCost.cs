using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeApprovedCost : InitiativeRelatedDbEntity
{
    public DateTimeOffset ApprovalDate { get; set; }
    public decimal Amount { get; set; }
    public string? SupportingDocument { get; set; }
}