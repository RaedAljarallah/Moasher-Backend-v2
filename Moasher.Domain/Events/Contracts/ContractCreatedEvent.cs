using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Contracts;

public class ContractCreatedEvent : DomainEvent
{
    public InitiativeContract Contract { get; }

    public ContractCreatedEvent(InitiativeContract contract)
    {
        Contract = contract;
    }
}