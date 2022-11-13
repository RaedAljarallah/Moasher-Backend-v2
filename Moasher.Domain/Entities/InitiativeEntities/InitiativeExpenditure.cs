using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Enums;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "مصروفات")]
public class InitiativeExpenditure : ApprovableDbEntity
{
    [Display(Name = "السنة")]
    public ushort Year { get; set; }
    
    [Display(Name = "الشهر")]
    public Month Month { get; set; }
    
    [Display(Name = "الصرف المخطط")]
    public decimal PlannedAmount { get; set; }
    
    [Display(Name = "الصرف الفعلي")]
    public decimal? ActualAmount { get; set; }
    
    [JsonIgnore]
    public InitiativeProject? Project { get; set; }
    public Guid? ProjectId { get; set; }
    [JsonIgnore]
    public InitiativeContract? Contract { get; set; }
    public Guid? ContractId { get; set; }

    public void MoveToContract(InitiativeContract contract)
    {
        Project = null;
        ProjectId = null;
        Contract = contract;
        ContractId = contract.Id;
    }
}