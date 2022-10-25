using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Utilities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesProgress;

public record GetInitiativesProgressQuery : IRequest<IEnumerable<InitiativeProgressDto>>
{
    public Guid Id { get; set; }
}

public class GetInitiativesProgressQueryHandler : IRequestHandler<GetInitiativesProgressQuery,
    IEnumerable<InitiativeProgressDto>>
{
    private readonly IMoasherDbContext _context;

    public GetInitiativesProgressQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InitiativeProgressDto>> Handle(GetInitiativesProgressQuery request,
        CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Include(i => i.Milestones)
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var milestones = initiative
            .Milestones
            .GroupBy(m => new
            {
                m.PlannedFinish.Year,
                m.PlannedFinish.Month
            })
            .OrderBy(m => m.Key.Year)
            .ThenBy(m => m.Key.Month)
            .Select(milestone => new
            {
                milestone.Key.Year,
                milestone.Key.Month,
                PlannedProgress = milestone.Sum(m => m.Weight)
            }).ToList();

        var achievedMilestones = initiative
            .Milestones.Where(m => m.ActualFinish.HasValue)
            .GroupBy(m => new
            {
                m.ActualFinish!.Value.Year,
                m.ActualFinish!.Value.Month
            })
            .OrderBy(m => m.Key.Year)
            .ThenBy(m => m.Key.Month)
            .Select(milestone => new
            {
                milestone.Key.Year,
                milestone.Key.Month,
                ActualProgress = milestone.Sum(m => m.Weight),
            }).ToList();

        var statusEnums = await _context.EnumTypes
            .Where(e => e.Category.ToLower() == EnumTypeCategory.InitiativeStatus.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var result = new List<InitiativeProgressDto>();
        var startDate = initiative.ActualStart ?? initiative.PlannedStart;
        var endDate = initiative.ActualFinish ?? initiative.PlannedFinish;
        var yearsMonthsRange = DateTimeService.GetYearsMonthsRange(startDate, endDate).ToList();
        var plannedProgressCumulative = 0f;
        var actualProgressCumulative = 0f;
        yearsMonthsRange.ForEach(range =>
        {
            var yearPlannedMilestones = milestones.Where(m => m.Year == range.Year).ToList();
            var yearAchievedMilestones = achievedMilestones.Where(m => m.Year == range.Year).ToList();
            range.Months.ToList().ForEach(month =>
            {
                var monthPlannedProgress = yearPlannedMilestones.FirstOrDefault(m => m.Month == month)?.PlannedProgress ?? 0;
                var monthActualProgress = yearAchievedMilestones.FirstOrDefault(m => m.Month == month)?.ActualProgress ?? 0;
                var dto = new InitiativeProgressDto
                {
                    Year = range.Year,
                    Month = (Month) month,
                    PlannedProgressCumulative = plannedProgressCumulative + monthPlannedProgress,
                    ActualProgressCumulative = actualProgressCumulative + monthActualProgress,
                };
                dto.SetStatusEnum(InitiativeUtility.CalculateStatus(
                    new Progress(dto.PlannedProgressCumulative, dto.ActualProgressCumulative), statusEnums));
                result.Add(dto);
                plannedProgressCumulative = dto.PlannedProgressCumulative;
                actualProgressCumulative = dto.ActualProgressCumulative;
            });
        });

        return result;
    }
}