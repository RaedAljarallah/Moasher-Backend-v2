using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "معالم")]
public class InitiativeMilestone : InitiativeRelatedDbEntity, ISchedulable
{
    [Display(Name = "اسم المعلم")]
    public string Name { get; set; } = default!;
    
    [Display(Name = "تاريخ الإنتهاء المخطط")]
    public DateTimeOffset PlannedFinish { get; set; }
    
    [Display(Name = "تاريخ الإنتهاء الفعلي")]
    public DateTimeOffset? ActualFinish { get; set; }
    
    [Display(Name = "الوزن")]
    public float Weight { get; set; }
    
    [Display(Name = "الوثيقة الداعمة")]
    public string? SupportingDocument { get; set; }
    
    public ICollection<ContractMilestone> ContractMilestones { get; set; }
        = new HashSet<ContractMilestone>();
}