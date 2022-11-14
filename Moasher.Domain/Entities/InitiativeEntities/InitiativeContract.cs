using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "عقود")]
public class InitiativeContract : InitiativeRelatedDbEntity
{
    private EnumType _statusEnum = default!;
    
    [Display(Name = "اسم العقد")]
    public string Name { get; set; } = default!;
    
    [Display(Name = "تاريخ البداية")]
    public DateTimeOffset StartDate { get; set; }
    
    [Display(Name = "تاريخ النهاية")]
    public DateTimeOffset EndDate { get; set; }
    
    [Display(Name = "المبلغ")]
    public decimal Amount { get; set; }
    
    [Display(Name = "الرقم المرجعي")]
    public string? RefNumber { get; set; }

    [Display(Name = "الحالة")]
    public string StatusName { get; private set; } = default!;
    public string StatusStyle { get; private set; } = default!;

    [JsonIgnore]
    public EnumType StatusEnum
    {
        get => _statusEnum;
        set
        {
            _statusEnum = value;
            StatusName = value.Name;
            StatusStyle = value.Style;
        }
    }
    
    public Guid? StatusEnumId { get; set; }
    
    [Display(Name = "المورد")]
    public string? Supplier { get; set; }
    
    [Display(Name = "تضمين قيمة العقد؟")]
    public bool CalculateAmount { get; set; }
    
    [Display(Name = "إجمالي المنصرف")]
    public decimal? TotalExpenditure { get; set; }
    
    [Display(Name = "منصرف السنة الحالية")]
    public decimal? CurrentYearExpenditure { get; set; }
    
    [Display(Name = "خطة الصرف متوازنة؟")]
    public bool BalancedExpenditurePlan { get; set; }
    public InitiativeProject? Project { get; set; }

    public ICollection<InitiativeExpenditure> Expenditures { get; set; }
        = new HashSet<InitiativeExpenditure>();

    public ICollection<InitiativeExpenditureBaseline> ExpendituresBaseline { get; set; }
        = new HashSet<InitiativeExpenditureBaseline>();
}