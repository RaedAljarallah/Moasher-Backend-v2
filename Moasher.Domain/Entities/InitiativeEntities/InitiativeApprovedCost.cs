using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "تكاليف معتمدة")]
public class InitiativeApprovedCost : InitiativeRelatedDbEntity
{
    [Display(Name = "تاريخ الاعتماد")]
    public DateTimeOffset ApprovalDate { get; set; }
    [Display(Name = "المبلغ")]
    public decimal Amount { get; set; }
    
    [Display(Name = "الوثيقة الداعمة")]
    public string? SupportingDocument { get; set; }
}