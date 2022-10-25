using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Utilities;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
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

        var gg = milestones.Where(m => m.Year == 2022).Where(m => m.Month == 10).ToList();
        var statusEnums = await _context.EnumTypes
            .Where(e => e.Category.ToLower() == EnumTypeCategory.InitiativeStatus.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var result = new List<InitiativesStatusProgressDto>();
        var startDate = initiatives.Min(i => i.ActualStart ?? i.PlannedStart);
        var endDate = initiatives.Max(i => i.ActualFinish ?? i.PlannedFinish);
        var endOfCurrentYearDate =
            new DateTimeOffset(new DateTime(DateTimeService.Now.Year, 12, 31), TimeSpan.FromHours(3));
        var yearsMonthsRange = DateTimeService
            .GetYearsMonthsRange(startDate, endDate < endOfCurrentYearDate ? endDate : endOfCurrentYearDate).ToList();
        var initiativesId = initiatives.Select(i => i.Id).ToList();
        var plannedProgressCumulative = 0f;
        var actualProgressCumulative = 0f;
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
                    var initiativeMonthPlannedProgressCumulative = monthPlannedMilestones
                        .Where(m => m.InitiativeId == initiativeId)
                        .Sum(m => m.PlannedProgress);
                    var initiativeMonthActualProgressCumulative = monthAchievedMilestones
                        .Where(m => m.InitiativeId == initiativeId)
                        .Sum(m => m.ActualProgress);

                    var monthStatus = InitiativeUtility.CalculateStatus(
                        new Progress(initiativeMonthPlannedProgressCumulative, initiativeMonthActualProgressCumulative),
                        statusEnums);
                    statuses.Add(monthStatus);
                });
            });
        });
        return result;
    }
}