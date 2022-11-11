using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Enums;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeExpenditureBaseline : ApprovableDbEntity
{
    public ushort Year { get; set; }
    public Month Month { get; set; }
    public decimal InitialPlannedAmount { get; set; }
    [JsonIgnore]
    public InitiativeProject? Project { get; set; }
    public Guid? ProjectId { get; set; }
    [JsonIgnore]
    public InitiativeContract? Contract { get; set; }
    public Guid? ContractId { get; set; }

    public static InitiativeExpenditureBaseline Map(InitiativeExpenditure expenditure)
    {
        return new InitiativeExpenditureBaseline
        {
            Year = expenditure.Year,
            Month = expenditure.Month,
            InitialPlannedAmount = expenditure.PlannedAmount,
            Project = expenditure.Project,
            ProjectId = expenditure.ProjectId,
            Contract = expenditure.Contract,
            ContractId = expenditure.ContractId
        };
    }
    
    public void MoveToContract(InitiativeContract contract)
    {
        Project = null;
        ProjectId = null;
        Contract = contract;
        ContractId = contract.Id;
    }
}