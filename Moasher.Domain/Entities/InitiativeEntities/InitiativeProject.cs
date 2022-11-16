using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
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

    [Display(Name = "المرحلة")]
    public string PhaseName { get; private set; } = default!;
    public string PhaseStyle { get; private set; } = default!;
    
    [JsonIgnore]
    public EnumType PhaseEnum
    {
        get => _phaseEnum;
        set
        {
            _phaseEnum = value;
            PhaseName = value.Name;
            PhaseStyle = value.Style;
        }
    }
    
    public Guid? PhaseEnumId { get; set; }
    
    [Display(Name = "تم التعاقد؟")]
    public bool Contracted { get; set; }
    public ICollection<InitiativeExpenditure> Expenditures { get; set; }
        = new HashSet<InitiativeExpenditure>();

    public ICollection<InitiativeExpenditureBaseline> ExpendituresBaseline { get; set; }
        = new HashSet<InitiativeExpenditureBaseline>();
    public ICollection<InitiativeProjectProgress> Progress { get; set; }
        = new HashSet<InitiativeProjectProgress>();
    
    public ICollection<ContractMilestone> ContractMilestones { get; set; }
        = new HashSet<ContractMilestone>();
    
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