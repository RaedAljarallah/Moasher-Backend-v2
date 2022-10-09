using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Extensions;

public static class InitiativeContractExtensions
{
    public static void SetTotalExpenditure(this InitiativeContract contract)
    {
        contract.TotalExpenditure = contract.GetTotalExpenditure();
    }

    public static void SetCurrentYearExpenditure(this InitiativeContract contract)
    {
        contract.CurrentYearExpenditure = contract.GetCurrentYearExpenditure();
    }

    internal static decimal? GetTotalExpenditure(this InitiativeContract contract)
    {
        if (!contract.Approved || !contract.Project.Approved)
        {
            return default;
        }

        return contract.Project.Expenditures
            .Where(e => e.Approved)
            .Sum(e => e.ActualAmount);
    }

    internal static decimal? GetCurrentYearExpenditure(this InitiativeContract contract)
    {
        if (!contract.Approved || !contract.Project.Approved)
        {
            return default;
        }

        return contract.Project.Expenditures
            .Where(e => e.Approved)
            .Where(e => e.Year == DateTimeOffset.UtcNow.AddHours(3).Year)
            .Sum(e => e.ActualAmount);
    }
}