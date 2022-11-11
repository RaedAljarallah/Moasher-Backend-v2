using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "آثار متوقعة")]
public class InitiativeImpact : InitiativeRelatedDbEntity
{
    public string Description { get; set; } = default!;
}