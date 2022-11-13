using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "مخرجات")]
public class InitiativeDeliverable : InitiativeRelatedDbEntity, ISchedulable
{
    [Display(Name = "اسم المخرج")]
    public string Name { get; set; } = default!;
    
    [Display(Name = "تاريخ الإنتهاء المخطط")]
    public DateTimeOffset PlannedFinish { get; set; }
    
    [Display(Name = "تاريخ الإنتهاء الفعلي")]
    public DateTimeOffset? ActualFinish { get; set; }
    
    [Display(Name = "الوثيقة الداعمة")]
    public string? SupportingDocument { get; set; }
}