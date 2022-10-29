using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Application.Features.Initiatives.Queries.GetInitiativesStatusProgress;
using Moasher.Domain.Common.Utilities;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.KPIs.Queries.GetKPIsStatusProgress;

public record GetKPIsStatusProgressQuery : IRequest<IEnumerable<KPIsStatusProgressDto>>
{
    public Guid? EntityId { get; set; }
    public Guid? ProgramId { get; set; }
}

public class
    GetKPIsStatusProgressQueryHandler : IRequestHandler<GetKPIsStatusProgressQuery, IEnumerable<KPIsStatusProgressDto>>
{
    private readonly IMoasherDbContext _context;

    public GetKPIsStatusProgressQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<KPIsStatusProgressDto>> Handle(GetKPIsStatusProgressQuery request,
        CancellationToken cancellationToken)
    {
        var kpis = await _context.KPIs
            .WithinParameters(new GetKPIsStatusProgressQueryParameter(request))
            .AsNoTracking()
            .Include(k => k.Values)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        if (!kpis.Any())
        {
            return new List<KPIsStatusProgressDto>();
        }

        var values = kpis
            .SelectMany(k => k.Values)
            .GroupBy(v => new
            {
                v.PlannedFinish.Year,
                v.PlannedFinish.Month,
                v.KPIId
            })
            .OrderBy(v => v.Key.Year)
            .ThenBy(v => v.Key.Month)
            .Select(value => new
            {
                value.Key.Year,
                value.Key.Month,
                value.Key.KPIId,
                PlannedProgress = value.Sum(v => v.TargetValue)
            }).ToList();

        var achievedValues = kpis
            .SelectMany(k => k.Values.Where(v => v.ActualFinish.HasValue))
            .GroupBy(v => new
            {
                v.ActualFinish!.Value.Year,
                v.ActualFinish!.Value.Month,
                v.KPIId
            })
            .OrderBy(v => v.Key.Year)
            .ThenBy(v => v.Key.Month)
            .Select(value => new
            {
                value.Key.Year,
                value.Key.Month,
                value.Key.KPIId,
                ActualProgress = value.Sum(v => v.ActualValue)
            }).ToList();

        var statusEnums = await _context.EnumTypes
            .Where(e => e.Category.ToLower() == EnumTypeCategory.KPIStatus.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var result = new List<KPIsStatusProgressDto>();
        var startDate = kpis.Min(k => k.StartDate);
        var endDate = kpis.Max(k => k.EndDate);
        var endOfCurrentYearDate =
            new DateTimeOffset(new DateTime(DateTimeService.Now.Year, 12, 31), TimeSpan.FromHours(3));
        var yearsMonthsRange = DateTimeService
            .GetYearsMonthsRange(startDate, endDate < endOfCurrentYearDate ? endDate : endOfCurrentYearDate).ToList();
        var kpisId = kpis.Select(k => k.Id).ToList();
        yearsMonthsRange.ForEach(range =>
        {
            var yearPlannedValues = values.Where(v => v.Year == range.Year).ToList();
            var yearAchievedValues = achievedValues.Where(v => v.Year == range.Year).ToList();
            range.Months.ToList().ForEach(month =>
            {
                var monthPlannedValues = yearPlannedValues.Where(v => v.Month <= month).ToList();
                var monthAchievedValues = yearAchievedValues.Where(v => v.Month <= month).ToList();
                var statuses = new List<EnumType?>();
                kpisId.ForEach(kpiId =>
                {
                    var kpi = kpis.First(k => k.Id == kpiId);

                    var limitFrom = new DateOnly(kpi.StartDate.Year, kpi.StartDate.Month, 1);
                    var limitTo = new DateOnly(kpi.EndDate.Year, kpi.EndDate.Month, 1);
                    var limit = new DateOnly(range.Year, month, 1);

                    if (limitFrom > limit || limitTo < limit) return;

                    var kpiMonthPlannedProgressCumulative = monthPlannedValues
                        .Where(v => v.KPIId == kpiId).ToList();

                    var kpiMonthActualProgressCumulative = monthAchievedValues
                        .Where(v => v.KPIId == kpiId).ToList();

                    if (!kpi.CalculateStatus)
                    {
                        statuses.Add(statusEnums.FirstOrDefault(e => e.Id == kpi.StatusEnumId));
                        return;
                    }

                    if (!kpiMonthPlannedProgressCumulative.Any() &&
                        !kpiMonthActualProgressCumulative.Any())
                    {
                        statuses.Add(statusEnums.FirstOrDefault(e => e.IsDefault));
                        return;
                    }

                    var plannedProgressCumulative = kpiMonthPlannedProgressCumulative.Sum(p => p.PlannedProgress);
                    var actualProgressCumulative = kpiMonthActualProgressCumulative.Sum(p => p.ActualProgress ?? 0);
                    var variance = plannedProgressCumulative - actualProgressCumulative;
                    var totalTargetValues = kpi.Values.Sum(v => v.TargetValue);
                    var progress = KPIUtility.CalculateProgress(plannedProgressCumulative, actualProgressCumulative,
                        totalTargetValues, variance, kpi.Polarity);
                    var monthStatus = KPIUtility.CalculateStatus(progress, statusEnums);
                    statuses.Add(monthStatus);
                });

                var dto = new KPIsStatusProgressDto
                {
                    Year = range.Year,
                    Month = (Month) month
                };

                statusEnums.OrderByDescending(e => e.IsDefault)
                    .ThenByDescending(e => e.LimitFrom)
                    .ToList()
                    .ForEach(status =>
                    {
                        dto.Progress.Add(new StatusProgressDto
                        {
                            Status = new EnumValue(status.Name, status.Style),
                            Count = statuses.Count(e => e?.Id == status.Id)
                        });
                    });

                result.Add(dto);
            });
        });

        return result;
    }
}