using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Utilities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Extensions;

public static class InitiativeExtensions
{
    public static decimal? GetCurrentYearBudget(this Initiative initiative)
    {
        return initiative.Budgets
            .Where(b => b.Approved)
            .Where(b => b.ApprovalDate.Year == DateTimeOffset.UtcNow.AddHours(3).Year)
            .MaxBy(a => a.ApprovalDate)?.Amount;
    }
    
    public static void SetCurrentYearBudget(this Initiative initiative)
    {
        initiative.CurrentYearBudget = initiative.GetCurrentYearBudget();
    }
    
    public static decimal? GetTotalBudget(this Initiative initiative)
    {
        return initiative.Budgets.Where(b => b.Approved).Sum(b => b.Amount);
    }
    
    public static void SetTotalBudget(this Initiative initiative)
    {
        initiative.TotalBudget = initiative.GetTotalBudget();
    }
    
    public static decimal? GetContractsAmount(this Initiative initiative)
    {
        return initiative.Contracts
            .Where(c => c.Approved)
            .Where(c => c.CalculateAmount)
            .Sum(c => c.Amount);
    }
    
    public static void SetContractsAmount(this Initiative initiative)
    {
        initiative.ContractsAmount = initiative.GetContractsAmount();
    }
    
    public static decimal? GetTotalExpenditure(this Initiative initiative)
    {
        return initiative.Contracts
            .Where(c => c.Approved)
            .Sum(c => c.TotalExpenditure);
    }

    public static void SetTotalExpenditure(this Initiative initiative)
    {
        initiative.TotalExpenditure = initiative.GetTotalExpenditure();
    }
    
    public static decimal? GetCurrentYearExpenditure(this Initiative initiative)
    {
        return initiative.Projects
            .Where(c => c.Approved)
            .SelectMany(p => p.Expenditures.Where(e => e.Year == DateTimeOffset.UtcNow.AddHours(3).Year))
            .Where(e => e.Approved)
            .Sum(e => e.ActualAmount);
    }
    
    public static void SetCurrentYearExpenditure(this Initiative initiative)
    {
        initiative.CurrentYearExpenditure = initiative.GetCurrentYearExpenditure();
    }
    
    public static decimal? GetTotalApprovedCost(this Initiative initiative)
    {
        return initiative.ApprovedCosts.Where(c => c.Approved).Sum(c => c.Amount);
    }
    
    public static void SetTotalApprovedCost(this Initiative initiative)
    {
        initiative.ApprovedCost = initiative.GetTotalApprovedCost();
    }
    
    public static Progress GetProgress(this Initiative initiative)
    {
        var actualProgress = 0f;
        var plannedProgress = 0f;
        foreach (var milestone in initiative.Milestones.Where(m => m.Approved))
        {
            if (milestone.ActualFinish.HasValue)
            {
                actualProgress += milestone.Weight;
            }

            if (milestone.PlannedFinish.Date <= DateTimeOffset.UtcNow.AddHours(3).Date)
            {
                plannedProgress += milestone.Weight;
            }
        }

        return new Progress(plannedProgress, actualProgress);
    }
    
    public static void SetProgress(this Initiative initiative)
    {
        var initiativeProgress = initiative.GetProgress();
        initiative.PlannedProgress = initiativeProgress.Planned;
        initiative.ActualProgress = initiativeProgress.Actual;
    }
    
    public static void SetStatus(this Initiative initiative, IEnumerable<EnumType> statusTypes)
    {
        initiative.SetProgress();
        if (!initiative.Milestones.Any(m => m.Approved))
        {
            initiative.StatusEnum = null;
        }
        else
        {
            var status = InitiativeUtility
                .CalculateStatus(new Progress(initiative.PlannedProgress ?? 0, initiative.ActualProgress ?? 0), statusTypes);

            initiative.StatusEnum = status;
        }

    }
    
    public static Analytic? GetLatestAnalytics(this Initiative initiative)
    {
        return initiative.Analytics.MaxBy(a => a.AnalyzedAt);
    }
    
    public static void SetLatestAnalytics(this Initiative initiative)
    {
        initiative.LatestAnalyticsModel = initiative.GetLatestAnalytics();
    }
}