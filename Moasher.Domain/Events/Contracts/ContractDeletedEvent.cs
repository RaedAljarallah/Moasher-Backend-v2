using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Contracts;

public class ContractDeletedEvent : DomainEvent
{
    public InitiativeContract Contract { get; }

    public ContractDeletedEvent(InitiativeContract contract)
    {
        Contract = contract;
    }
}