using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Budgets;

public class BudgetDeletedEvent : DomainEvent
{
    public InitiativeBudget Budget { get; }

    public BudgetDeletedEvent(InitiativeBudget budget)
    {
        Budget = budget;
    }
}