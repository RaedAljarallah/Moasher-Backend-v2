using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Contracts;

public class ContractUpdatedEvent : DomainEvent
{
    public InitiativeContract Contract { get; }

    public ContractUpdatedEvent(InitiativeContract contract)
    {
        Contract = contract;
    }
}