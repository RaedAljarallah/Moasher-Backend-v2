using Moasher.Domain.Common.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeProjectBaseline : AuditableDbEntity
{
    public DateTimeOffset InitialPlannedContractingDate { get; set; }
    public decimal InitialEstimatedAmount { get; set; }
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