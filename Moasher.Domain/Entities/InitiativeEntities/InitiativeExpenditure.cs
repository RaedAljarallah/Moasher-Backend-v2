using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeExpenditure : ApprovableDbEntity
{
    public ushort Year { get; set; }
    public Month Month { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal? ActualAmount { get; set; }
    public InitiativeProject? Project { get; set; }
    public Guid? ProjectId { get; set; }
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