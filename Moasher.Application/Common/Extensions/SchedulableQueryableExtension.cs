﻿using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Enums;

namespace Moasher.Application.Common.Extensions;

public static class SchedulableQueryableExtension
{
    public static IQueryable<TEntity> WithinSchedulableParameters<TEntity>(this IQueryable<ISchedulable> query,
        SchedulableQueryParametersDto parameter)
        where TEntity : ISchedulable
    {
        if (!string.IsNullOrWhiteSpace(parameter.St))
        {
            if (string.Equals(parameter.St, SchedulableStatus.Planned.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Planned();
            }
            
            if (string.Equals(parameter.St, SchedulableStatus.Completed.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Completed();
            }
            
            if (string.Equals(parameter.St, SchedulableStatus.Late.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Late();
            }
            
            if (string.Equals(parameter.St, SchedulableStatus.Uncompleted.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Uncompleted();
            }
            
            if (string.Equals(parameter.St, SchedulableStatus.Due.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.Due();
            }
        }
        
        if (parameter.DueUntil.HasValue)
        {
            query = query.DueUntil(parameter.DueUntil.Value);
        }
        
        if (parameter.PlannedFrom.HasValue)
        {
            query = query.PlannedFrom(parameter.PlannedFrom.Value);
        }
        
        if (parameter.PlannedTo.HasValue)
        {
            query = query.PlannedTo(parameter.PlannedTo.Value);
        }

        if (parameter.ActualFrom.HasValue)
        {
            query = query.ActualFrom(parameter.ActualFrom.Value);
        }

        if (parameter.ActualTo.HasValue)
        {
            query = query.ActualTo(parameter.ActualTo.Value);
        }

        return query.Cast<TEntity>();
    }
    
    private static IQueryable<ISchedulable> Planned(this IQueryable<ISchedulable> query)
        {
            return query
                .Where(m => m.PlannedFinish >= DateTimeService.Now)
                .Where(m => !m.ActualFinish.HasValue);
        }

        private static IQueryable<ISchedulable> Completed(this IQueryable<ISchedulable> query)
        {
            return query.Where(m => m.ActualFinish.HasValue);
        }

        private static IQueryable<ISchedulable> Late(this IQueryable<ISchedulable> query)
        {
            return query
                .Where(m => m.PlannedFinish < DateTimeService.Now)
                .Where(m => !m.ActualFinish.HasValue);
        }

        private static IQueryable<ISchedulable> Uncompleted(this IQueryable<ISchedulable> query)
        {
            return query.Where(m => !m.ActualFinish.HasValue);
        }

        private static IQueryable<ISchedulable> Due(this IQueryable<ISchedulable> query)
        {
            var currentQuarterMonths = DateTimeService.GetCurrentQuarterMonths();
            return query
                .Where(m => m.PlannedFinish.Month >= currentQuarterMonths.First())
                .Where(m => m.PlannedFinish.Month <= currentQuarterMonths.Last())
                .Where(m => !m.ActualFinish.HasValue);
        }

        private static IQueryable<ISchedulable> DueUntil(this IQueryable<ISchedulable> query, DateTimeOffset dueDate)
        {
            return query
                .Where(m => m.PlannedFinish >= DateTimeService.Now)
                .Where(m => m.PlannedFinish <= dueDate)
                .Where(m => !m.ActualFinish.HasValue);
        }

        private static IQueryable<ISchedulable> PlannedFrom(this IQueryable<ISchedulable> query, DateTimeOffset fromDate)
        {
            return query.Where(m => m.PlannedFinish >= fromDate);
        }

        private static IQueryable<ISchedulable> PlannedTo(this IQueryable<ISchedulable> query, DateTimeOffset toDate)
        {
            return query.Where(m => m.PlannedFinish <= toDate);
        }

        private static IQueryable<ISchedulable> ActualFrom(this IQueryable<ISchedulable> query, DateTimeOffset fromDate)
        {
            return query
                .Where(m => m.ActualFinish.HasValue)
                .Where(m => m.ActualFinish >= fromDate);
        }

        private static IQueryable<ISchedulable> ActualTo(this IQueryable<ISchedulable> query, DateTimeOffset toDate)
        {
            return query
                .Where(m => m.ActualFinish.HasValue)
                .Where(m => m.ActualFinish <= toDate);
        }
}