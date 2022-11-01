using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Types;

namespace Moasher.Application.Common.Services;

public static class SchedulableEntityService
{
    public static string? GetStatus(DateTimeOffset plannedFinish, DateTimeOffset? actualFinish)
    {
        var currentDate = LocalDateTime.Now.Date;
        if (plannedFinish >= currentDate && !actualFinish.HasValue)
        {
            return SchedulableStatus.Planned.ToString().ToLower();
        }

        if (actualFinish.HasValue)
        {
            return SchedulableStatus.Completed.ToString().ToLower();
        }

        if (plannedFinish < currentDate && !actualFinish.HasValue)
        {
            return SchedulableStatus.Late.ToString().ToLower();
        }

        if (!actualFinish.HasValue)
        {
            return SchedulableStatus.Uncompleted.ToString().ToLower();
        }

        return null;
    }
    
    public static SchedulableDto GenerateSummary(IList<ISchedulableSummary> items)
    {
        var planned = 0;
        var completed = 0;
        var late = 0;
        var uncompleted = 0;

        items.ToList().ForEach(i =>
        {
            if (i.Status?.ToLower() == SchedulableStatus.Planned.ToString().ToLower())
            {
                planned += 1;
            }
            
            if (i.Status?.ToLower() == SchedulableStatus.Completed.ToString().ToLower())
            {
                completed += 1;
            }
            
            if (i.Status?.ToLower() == SchedulableStatus.Late.ToString().ToLower())
            {
                late += 1;
            }
            
            if (i.Status?.ToLower() == SchedulableStatus.Uncompleted.ToString().ToLower())
            {
                uncompleted += 1;
            }
        });

        return new SchedulableDto
        {
            All = items.Count,
            Planned = planned,
            Completed = completed,
            Late = late,
            Uncompleted = uncompleted
        };
    }
}