using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Budgets;

public class BudgetUpdatedEvent : DomainEvent
{
    public InitiativeBudget Budget { get; }

    public BudgetUpdatedEvent(InitiativeBudget budget)
    {
        Budget = budget;
    }
}