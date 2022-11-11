using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Enums;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "مصروفات")]
public class InitiativeExpenditure : ApprovableDbEntity
{
    public ushort Year { get; set; }
    public Month Month { get; set; }
    public decimal PlannedAmount { get; set; }
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