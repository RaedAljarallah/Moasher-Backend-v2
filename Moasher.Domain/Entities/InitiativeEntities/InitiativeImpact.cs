using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeImpact : InitiativeRelatedDbEntity
{
    public string Description { get; set; } = default!;
}