using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.ApprovedCosts;

public class ApprovedCostUpdatedEvent : DomainEvent
{
    public InitiativeApprovedCost ApprovedCost { get; }

    public ApprovedCostUpdatedEvent(InitiativeApprovedCost approvedCost)
    {
        ApprovedCost = approvedCost;
    }
}