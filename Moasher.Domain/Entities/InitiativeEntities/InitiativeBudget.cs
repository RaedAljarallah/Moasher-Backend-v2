using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "ميزانيات")]
public class InitiativeBudget : InitiativeRelatedDbEntity
{
    [Display(Name = "تاريخ الاعتماد")]
    public DateTimeOffset ApprovalDate { get; set; }
    [Display(Name = "المبلغ المحدث")]
    public decimal Amount { get; set; }
    
    [Display(Name = "المبلغ الأصلي")]
    public decimal InitialAmount { get; set; }
    
    [Display(Name = "الوثيقة الداعمة")]
    public string? SupportingDocument { get; set; }
}