using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Common.Extensions;

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

    public static decimal GetPlannedExpenditureToDate(this InitiativeContract contract)
    {
        if (!contract.Approved)
        {
            return 0;
        }
        
        var currentDate = DateTimeOffset.UtcNow.AddHours(3);
        return contract.Expenditures
            .Where(e => e.Approved)
            .Where(e => e.Year <= DateTimeOffset.UtcNow.AddHours(3).Year)
            .Where(e => new DateTime(e.Year, (int) e.Month, 1) <= new DateTime(currentDate.Year, currentDate.Month, 1))
            .Sum(e => e.PlannedAmount);
    }
    
    internal static decimal? GetTotalExpenditure(this InitiativeContract contract)
    {
        if (!contract.Approved)
        {
            return default;
        }

        return contract.Expenditures
            .Where(e => e.Approved)
            .Sum(e => e.ActualAmount);
    }

    internal static decimal? GetCurrentYearExpenditure(this InitiativeContract contract)
    {
        if (!contract.Approved)
        {
            return default;
        }

        return contract.Expenditures
            .Where(e => e.Approved)
            .Where(e => e.Year == DateTimeOffset.UtcNow.AddHours(3).Year)
            .Sum(e => e.ActualAmount);
    }
}