using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Budgets;

public class BudgetCreatedEvent : DomainEvent
{
    public InitiativeBudget Budget { get; }

    public BudgetCreatedEvent(InitiativeBudget budget)
    {
        Budget = budget;
    }
}