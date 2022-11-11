using Moasher.Domain.Common.Abstracts;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeProjectBaseline : ApprovableDbEntity
{
    public DateTimeOffset InitialPlannedContractingDate { get; set; }
    public decimal InitialEstimatedAmount { get; set; }
    [JsonIgnore]
    public InitiativeProject Project { get; set; } = default!;
    public Guid ProjectId { get; set; }

    public static InitiativeProjectBaseline Map(InitiativeProject project)
    {
        return new InitiativeProjectBaseline
        {
            InitialPlannedContractingDate = project.PlannedContractingDate,
            InitialEstimatedAmount = project.EstimatedAmount,
            Project = project,
            ProjectId = project.Id
        };
    }
}