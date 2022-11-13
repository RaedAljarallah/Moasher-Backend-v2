using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Moasher.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "مشاريع")]
public class InitiativeProject : InitiativeRelatedDbEntity
{
    private EnumType _phaseEnum = default!;
    
    [Display(Name = "اسم المشروع")]
    public string Name { get; set; } = default!;
    
    [Display(Name = "تاريخ الطرح المخطط")]
    public DateTimeOffset PlannedBiddingDate { get; set; }
    
    [Display(Name = "تاريخ الطرح الفعلي")]
    public DateTimeOffset? ActualBiddingDate { get; set; }
    
    [Display(Name = "تاريخ التعاقد المخطط")]
    public DateTimeOffset PlannedContractingDate { get; set; }
    
    [Display(Name = "تاريخ التعاقد الفعلي")]
    public DateTimeOffset? ActualContractingDate { get; set; }
    
    [Display(Name = "تاريخ نهاية العقد المخطط")]
    public DateTimeOffset PlannedContractEndDate { get; set; }
    
    [Display(Name = "القيمة التقديرية")]
    public decimal EstimatedAmount { get; set; }
    
    [JsonIgnore]
    public EnumValue Phase { get; private set; } = default!;
    
    [JsonIgnore]
    public EnumType PhaseEnum
    {
        get => _phaseEnum;
        set
        {
            _phaseEnum = value;
            Phase = new EnumValue(value.Name, value.Style);
        }
    }
    
    [Display(Name = "المرحلة")]
    public Guid? PhaseEnumId { get; set; }
    
    [Display(Name = "تم التعاقد؟")]
    public bool Contracted { get; set; }
    public ICollection<InitiativeExpenditure> Expenditures { get; set; }
        = new HashSet<InitiativeExpenditure>();

    public ICollection<InitiativeExpenditureBaseline> ExpendituresBaseline { get; set; }
        = new HashSet<InitiativeExpenditureBaseline>();
    public ICollection<InitiativeProjectProgress> Progress { get; set; }
        = new HashSet<InitiativeProjectProgress>();
    [JsonIgnore]
    public InitiativeContract? Contract { get; set; }
    public Guid? ContractId { get; set; }
    
    public InitiativeProjectBaseline Baseline { get; set; } = default!;
    public void Contracting(DateTimeOffset contractingDate)
    {
        Contracted = true;
        ActualContractingDate = contractingDate;
    }
}