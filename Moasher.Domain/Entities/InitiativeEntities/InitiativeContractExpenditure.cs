using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeContractExpenditure : InitiativeRelatedDbEntity
{
    public ushort Year { get; set; }
}