using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Utilities;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.KPIs.Queries.GetKPIProgress;

public record GetKPIProgressQuery : IRequest<IEnumerable<KPIProgressDto>>
{
    public Guid Id { get; set; }
}

public class GetKPIProgressQueryHandler : IRequestHandler<GetKPIProgressQuery, IEnumerable<KPIProgressDto>>
{
    private readonly IMoasherDbContext _context;

    public GetKPIProgressQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<KPIProgressDto>> Handle(GetKPIProgressQuery request, CancellationToken cancellationToken)
    {
        var kpi = await _context.KPIs
            .AsNoTracking()
            .Include(k => k.Values)
            .AsSplitQuery()
            .FirstOrDefaultAsync(k => k.Id == request.Id, cancellationToken);

        if (kpi is null)
        {
            throw new NotFoundException();
        }

        var values = kpi
            .Values
            .GroupBy(v => new
            {
                v.PlannedFinish.Year,
                v.PlannedFinish.Month
            })
            .OrderBy(v => v.Key.Year)
            .ThenBy(v => v.Key.Month)
            .Select(value => new
            {
                value.Key.Year,
                value.Key.Month,
                PlannedProgress = value.Sum(v => v.TargetValue)
            }).ToList();

        var achievedValues = kpi
            .Values.Where(v => v.ActualFinish.HasValue)
            .GroupBy(v => new
            {
                v.ActualFinish!.Value.Year,
                v.ActualFinish!.Value.Month
            })
            .OrderBy(v => v.Key.Year)
            .ThenBy(v => v.Key.Month)
            .Select(value => new
            {
                value.Key.Year,
                value.Key.Month,
                ActualProgress = value.Sum(v => v.ActualValue)
            }).ToList();
        
        var statusEnums = await _context.EnumTypes
            .Where(e => e.Category.ToLower() == EnumTypeCategory.KPIStatus.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var result = new List<KPIProgressDto>();
        var yearsMonthsRange = DateTimeService.GetYearsMonthsRange(kpi.StartDate, kpi.EndDate).ToList();
        var plannedProgressCumulative = 0f;
        var actualProgressCumulative = 0f;
        var totalTargetValues = kpi.Values.Sum(v => v.TargetValue);
        yearsMonthsRange.ForEach(range =>
        {
            var yearPlannedMilestones = values.Where(v => v.Year == range.Year).ToList();
            var yearAchievedMilestones = achievedValues.Where(v => v.Year == range.Year).ToList();
            range.Months.ToList().ForEach(month =>
            {
                var monthPlannedProgress = yearPlannedMilestones.FirstOrDefault(v => v.Month == month)?.PlannedProgress ?? 0;
                var monthActualProgress = yearAchievedMilestones.FirstOrDefault(v => v.Month == month)?.ActualProgress ?? 0;
                var dto = new KPIProgressDto
                {
                    Year = range.Year,
                    Month = (Month) month
                };
                
                plannedProgressCumulative += monthPlannedProgress;
                actualProgressCumulative += monthActualProgress;
                var variance = plannedProgressCumulative - actualProgressCumulative;
                var progress = KPIUtility.CalculateProgress(plannedProgressCumulative, actualProgressCumulative,
                    totalTargetValues, variance, kpi.Polarity);

                dto.PlannedProgressCumulative = progress.Planned;
                dto.ActualProgressCumulative = progress.Actual;
                dto.SetStatusEnum(KPIUtility.CalculateStatus(
                    new Progress(dto.PlannedProgressCumulative, dto.ActualProgressCumulative), statusEnums));
                result.Add(dto);
            });
        });
        
        return result;
    }
}