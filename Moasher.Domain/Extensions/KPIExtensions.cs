﻿using Moasher.Domain.Entities;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Utilities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Extensions;

public static class KPIExtensions
{
    public static Progress GetProgress(this KPI kpi)
    {
        var actualValuesSum = 0f;
        var plannedValuesSum = 0f;
            
        var polarity = kpi.Polarity;
        var variance = 0f;

        var totalTargetValues = kpi.Values.Where(v => v.Approved).Sum(v => v.TargetValue);
        foreach (var value in kpi.Values.Where(v => v.Approved))
        {
            if (value.ActualFinish.HasValue)
            {
                actualValuesSum += value.ActualValue ?? 0f;
                variance += value.TargetValue - (value.ActualValue ?? 0f);
            }

            if (value.PlannedFinish.Date <= DateTimeOffset.UtcNow.AddHours(3).Date)
            {
                plannedValuesSum += value.TargetValue;
            }
        }

        return KPIUtility.CalculateProgress(plannedValuesSum, actualValuesSum, totalTargetValues, variance, polarity);
    }

    public static void SetProgress(this KPI kpi)
    {
        var kpiProgress = kpi.GetProgress();
        kpi.PlannedProgress = kpiProgress.Planned;
        kpi.ActualProgress = kpiProgress.Actual;
    }

    public static void SetStatus(this KPI kpi, IEnumerable<EnumType> statusTypes)
    {
        kpi.SetProgress();
        if (!kpi.Values.Any(v => v.Approved))
        {
            kpi.StatusEnum = null;
        }
        else
        {
            var status = KPIUtility.CalculateStatus(new Progress(kpi.PlannedProgress ?? 0, kpi.ActualProgress ?? 0), statusTypes);

            kpi.StatusEnum = status;
        }
    }

    public static Analytic? GetLatestAnalytics(this KPI kpi)
    {
        return kpi.Analytics.MaxBy(a => a.AnalyzedAt);
    }

    public static void SetLatestAnalytics(this KPI kpi)
    {
        kpi.LatestAnalyticsModel = kpi.GetLatestAnalytics();
    }
}