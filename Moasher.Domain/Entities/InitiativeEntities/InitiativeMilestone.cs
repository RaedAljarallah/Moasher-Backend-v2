using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "معالم")]
public class InitiativeMilestone : InitiativeRelatedDbEntity, ISchedulable
{
    public string Name { get; set; } = default!;
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public float Weight { get; set; }
    public string? SupportingDocument { get; set; }
    
    // public ICollection<ContractMilestone> ContractMilestones { get; set; }
    //     = new HashSet<ContractMilestone>();
}