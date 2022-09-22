using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.ApprovedCosts;

public class ApprovedCostCreatedEvent : DomainEvent
{
    public InitiativeApprovedCost ApprovedCost { get; }

    public ApprovedCostCreatedEvent(InitiativeApprovedCost approvedCost)
    {
        ApprovedCost = approvedCost;
    }
}