using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "المعالم المرتبطة")]
public class ContractMilestone : ApprovableDbEntity
{
    private InitiativeMilestone _milestone = default!;
    private InitiativeProject? _project;
    private InitiativeContract? _contract;

    [Display(Name = "اسم المعلم")] 
    public string MilestoneName { get; private set; } = default!;
    
    [JsonIgnore]
    public InitiativeMilestone Milestone
    {
        get => _milestone;
        set
        {
            _milestone = value;
            MilestoneName = value.Name;
        }
    }
    public Guid MilestoneId { get; set; }

    [Display(Name = "اسم المشروع")]
    public string? ProjectName { get; private set; }
    
    [JsonIgnore]
    public InitiativeProject? Project
    {
        get => _project;
        set
        {
            _project = value;
            ProjectName = value?.Name;
        }
    }
    public Guid? ProjectId { get; set; }

    [Display(Name = "اسم العقد")]
    public string? ContractName { get; set; }
    
    [JsonIgnore]
    public InitiativeContract? Contract
    {
        get => _contract;
        set
        {
            _contract = value;
            ContractName = value?.Name;
        }
    }

    public Guid? ContractId { get; set; }

    public void MoveToContract(InitiativeContract contract)
    {
        Project = null;
        ProjectId = null;
        Contract = contract;
        ContractId = contract.Id;
    }
}