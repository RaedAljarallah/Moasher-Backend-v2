using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Utilities;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.Types;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesStatusProgress;

public record GetInitiativesStatusProgressQuery : IRequest<IEnumerable<InitiativesStatusProgressDto>>
{
    public Guid? EntityId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? PortfolioId { get; set; }
}

public class GetInitiativesStatusProgressQueryHandler : IRequestHandler<GetInitiativesStatusProgressQuery,
    IEnumerable<InitiativesStatusProgressDto>>
{
    private readonly IMoasherDbContext _context;

    public GetInitiativesStatusProgressQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InitiativesStatusProgressDto>> Handle(GetInitiativesStatusProgressQuery request,
        CancellationToken cancellationToken)
    {
        var initiatives = await _context.Initiatives
            .WithinParameters(new GetInitiativesStatusProgressQueryParameter(request))
            .AsNoTracking()
            .Include(i => i.Milestones)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        if (!initiatives.Any())
        {
            return new List<InitiativesStatusProgressDto>();
        }

        var milestones = initiatives
            .SelectMany(i => i.Milestones)
            .GroupBy(m => new
            {
                m.PlannedFinish.Year,
                m.PlannedFinish.Month,
                m.InitiativeId
            })
            .OrderBy(m => m.Key.Year)
            .ThenBy(m => m.Key.Month)
            .Select(milestone => new
            {
                milestone.Key.Year,
                milestone.Key.Month,
                milestone.Key.InitiativeId,
                PlannedProgress = milestone.Sum(m => m.Weight)
            }).ToList();

        var achievedMilestones = initiatives
            .SelectMany(i => i.Milestones.Where(m => m.ActualFinish.HasValue))
            .GroupBy(m => new
            {
                m.ActualFinish!.Value.Year,
                m.ActualFinish!.Value.Month,
                m.InitiativeId
            })
            .OrderBy(m => m.Key.Year)
            .ThenBy(m => m.Key.Month)
            .Select(milestone => new
            {
                milestone.Key.Year,
                milestone.Key.Month,
                milestone.Key.InitiativeId,
                ActualProgress = milestone.Sum(m => m.Weight)
            }).ToList();

        var statusEnums = await _context.EnumTypes
            .Where(e => e.Category.ToLower() == EnumTypeCategory.InitiativeStatus.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var result = new List<InitiativesStatusProgressDto>();
        var startDate = initiatives.Min(i => i.ActualStart ?? i.PlannedStart);
        var endDate = initiatives.Max(i => i.ActualFinish ?? i.PlannedFinish);
        var endOfCurrentYearDate =
            new DateTimeOffset(new DateTime(LocalDateTime.Now.Year, 12, 31), TimeSpan.FromHours(3));
        var yearsMonthsRange = DateTimeService
            .GetYearsMonthsRange(startDate, endDate < endOfCurrentYearDate ? endDate : endOfCurrentYearDate).ToList();
        var initiativesId = initiatives.Select(i => i.Id).ToList();

        yearsMonthsRange.ForEach(range =>
        {
            var yearPlannedMilestones = milestones.Where(m => m.Year == range.Year).ToList();
            var yearAchievedMilestones = achievedMilestones.Where(m => m.Year == range.Year).ToList();
            range.Months.ToList().ForEach(month =>
            {
                var monthPlannedMilestones = yearPlannedMilestones.Where(m => m.Month <= month).ToList();
                var monthAchievedMilestones = yearAchievedMilestones.Where(m => m.Month <= month).ToList();
                var statuses = new List<EnumType?>();
                initiativesId.ForEach(initiativeId =>
                {
                    var initiative = initiatives.First(i => i.Id == initiativeId);
                    var initiativeStartDate = initiative.ActualStart ?? initiative.PlannedStart;
                    var initiativeEndDate = initiative.ActualFinish ?? initiative.PlannedFinish;

                    var limitFrom = new DateOnly(initiativeStartDate.Year, initiativeStartDate.Month, 1);
                    var limitTo = new DateOnly(initiativeEndDate.Year, initiativeEndDate.Month, 1);
                    var limit = new DateOnly(range.Year, month, 1);

                    if (limitFrom > limit || limitTo < limit) return;

                    var initiativeMonthPlannedProgressCumulative = monthPlannedMilestones
                        .Where(m => m.InitiativeId == initiativeId).ToList();

                    var initiativeMonthActualProgressCumulative = monthAchievedMilestones
                        .Where(m => m.InitiativeId == initiativeId).ToList();

                    if (!initiative.CalculateStatus)
                    {
                        statuses.Add(statusEnums.FirstOrDefault(e => e.Id == initiative.StatusEnumId));
                        return;
                    }

                    if (!initiativeMonthPlannedProgressCumulative.Any() &&
                        !initiativeMonthActualProgressCumulative.Any())
                    {
                        statuses.Add(statusEnums.FirstOrDefault(e => e.IsDefault));
                        return;
                    }

                    var plannedProgress = initiativeMonthPlannedProgressCumulative.Sum(p => p.PlannedProgress);
                    var actualProgress = initiativeMonthActualProgressCumulative.Sum(p => p.ActualProgress);
                    var monthStatus =
                        InitiativeUtility.CalculateStatus(new Progress(plannedProgress, actualProgress), statusEnums);
                    statuses.Add(monthStatus);
                });

                var dto = new InitiativesStatusProgressDto
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