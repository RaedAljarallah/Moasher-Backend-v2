﻿using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public Guid? InitiativeId { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? PortfolioId { get; set; }
    public Guid? ProgramId { get; set; }
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
        var initiatives = await _context.Initiatives
            .WithinParameters(new GetInitiativesProgressQueryParameter(request))
            .AsNoTracking()
            .Include(i => i.Milestones)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        if (!initiatives.Any())
        {
            return new List<InitiativeProgressDto>();
        }
        
        var milestones = initiatives
            .SelectMany(i => i.Milestones)
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
                PlannedProgress = milestone.Sum(m => m.Weight),
            }).ToList();

        var achievedMilestones = initiatives
            .SelectMany(i => i.Milestones.Where(m => m.ActualFinish.HasValue))
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
        var months = Enum.GetValues<Month>().ToList();
        var startYear = initiatives.Min(i => i.ActualStart?.Year ?? i.PlannedStart.Year);
        var lastYear = initiatives.Max(i => i.ActualFinish?.Year ?? i.PlannedFinish.Year);
        var years = Enumerable.Range(startYear, Math.Min(DateTimeService.Now.Year, lastYear) - startYear + 1)
            .OrderBy(y => y)
            .ToList();

        var plannedProgressCumulative = 0f;
        var actualProgressCumulative = 0f;
        years.ForEach(year =>
        {
            var yearMilestones = milestones.Where(m => m.Year == year).ToList();
            var yearAchievedMilestones = achievedMilestones.Where(m => m.Year == year).ToList();
            months.ForEach(month =>
            {
                var monthPlannedProgress =
                    (yearMilestones.FirstOrDefault(m => m.Month == (int) month)?.PlannedProgress ?? 0) / (initiatives.Count * 100) * 100;
                var monthActualProgress =
                    (yearAchievedMilestones.FirstOrDefault(m => m.Month == (int) month)?.ActualProgress ?? 0) / (initiatives.Count * 100) * 100;
                var dto = new InitiativeProgressDto
                {
                    Year = year,
                    Month = month,
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