using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.ApprovedCosts;

public class ApprovedCostDeletedEvent : DomainEvent
{
    public InitiativeApprovedCost ApprovedCost { get; }

    public ApprovedCostDeletedEvent(InitiativeApprovedCost approvedCost)
    {
        ApprovedCost = approvedCost;
    }
}