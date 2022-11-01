using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Types;

namespace Moasher.Domain.Common.Extensions;

public static class InitiativeProjectExtensions
{
    public static decimal GetPlannedExpenditureToDate(this InitiativeProject project)
    {
        if (!project.Approved)
        {
            return 0;
        }
        
        var currentDate = LocalDateTime.Now;
        return project.Expenditures
            .Where(e => e.Approved)
            .Where(e => e.Year <= LocalDateTime.Now.Year)
            .Where(e => new DateTime(e.Year, (int) e.Month, 1) <= new DateTime(currentDate.Year, currentDate.Month, 1))
            .Sum(e => e.PlannedAmount);
    }
}