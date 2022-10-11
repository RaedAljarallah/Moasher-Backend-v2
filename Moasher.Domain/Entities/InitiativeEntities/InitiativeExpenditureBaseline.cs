using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeExpenditureBaseline : AuditableDbEntity
{
    public ushort Year { get; set; }
    public Month Month { get; set; }
    public decimal InitialPlannedAmount { get; set; }
    public InitiativeProject Project { get; set; } = default!;
    public Guid ProjectId { get; set; }

    public static InitiativeExpenditureBaseline Map(InitiativeExpenditure expenditure)
    {
        return new InitiativeExpenditureBaseline
        {
            Year = expenditure.Year,
            Month = expenditure.Month,
            InitialPlannedAmount = expenditure.PlannedAmount,
            Project = expenditure.Project,
            ProjectId = expenditure.ProjectId
        };
    }
}