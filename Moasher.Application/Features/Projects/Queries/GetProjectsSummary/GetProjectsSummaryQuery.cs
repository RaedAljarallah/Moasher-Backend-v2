using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.Projects.Queries.GetProjectsSummary;

public record GetProjectsSummaryQuery : IRequest<IEnumerable<ProjectsSummaryDto>>
{
    public Guid InitiativeId { get; set; }
    public ushort? Year { get; set; }
}

public class GetProjectsSummaryQueryHandler : IRequestHandler<GetProjectsSummaryQuery, IEnumerable<ProjectsSummaryDto>>
{
    private readonly IMoasherDbContext _context;

    public GetProjectsSummaryQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProjectsSummaryDto>> Handle(GetProjectsSummaryQuery request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .AsNoTracking()
            .Select(initiative => new
            {
                initiative.Id,
                initiative.PlannedStart,
                initiative.PlannedFinish,
                initiative.ActualStart,
                initiative.ActualFinish,
                Projects = initiative.Projects
                    .Where(p => p.Approved)
                    .Where(p => !request.Year.HasValue ||
                                (request.Year.HasValue && p.PlannedContractingDate.Year == request.Year))
                    .ToList(),
                Baselines = initiative.Projects
                    .Where(p => p.Approved)
                    .Where(p => !request.Year.HasValue ||
                                (request.Year.HasValue && p.PlannedContractingDate.Year == request.Year))
                    .Select(p => p.Baseline)
                    .ToList(),
                Contracts = initiative.Contracts
                    .Where(c => c.Approved)
                    .Where(c => !request.Year.HasValue || (request.Year.HasValue && c.StartDate.Year == request.Year))
                    .ToList(),
                ApprovedCosts = initiative.ApprovedCosts
                    .Where(a => a.Approved)
                    .Where(a => !request.Year.HasValue || (request.Year.HasValue && a.ApprovalDate.Year <= request.Year))
                    .ToList()
            })
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);
        
        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var projects = initiative.Projects
            .GroupBy(p => new
            {
                p.PlannedContractingDate.Year,
                p.PlannedContractingDate.Month
            })
            .OrderBy(p => p.Key.Year)
            .ThenBy(p => p.Key.Month)
            .Select(project => new
            {
                project.Key.Year,
                project.Key.Month,
                Amount = project.Sum(p => p.EstimatedAmount)
            }).ToList();

        var baselines = initiative.Baselines
            .GroupBy(b => new
            {
                b.InitialPlannedContractingDate.Year,
                b.InitialPlannedContractingDate.Month
            })
            .OrderBy(b => b.Key.Year)
            .ThenBy(b => b.Key.Month)
            .Select(baseline => new
            {
                baseline.Key.Year,
                baseline.Key.Month,
                Amount = baseline.Sum(b => b.InitialEstimatedAmount)
            }).ToList();

        var contracts = initiative.Contracts
            .GroupBy(c => new
            {
                c.StartDate.Year,
                c.StartDate.Month
            })
            .OrderBy(c => c.Key.Year)
            .ThenBy(c => c.Key.Month)
            .Select(contract => new
            {
                contract.Key.Year,
                contract.Key.Month,
                Amount = contract.Sum(c => c.Amount)
            }).ToList();
    
        
        var result = new List<ProjectsSummaryDto>();
        var months = Enum.GetValues<Month>().ToList();
        var startYear = initiative.ActualStart?.Year ?? initiative.PlannedStart.Year;
        var lastYear = initiative.ActualFinish?.Year ?? initiative.PlannedFinish.Year;
        var years = Enumerable.Range(startYear, Math.Min(DateTimeService.Now.Year, lastYear) - startYear + 1)
            .OrderBy(y => y)
            .ToList();
        
        var initialPlannedAmountCumulative = 0m;
        var plannedAmountCumulative = 0m;
        var actualAmountCumulative = 0m;
        years.ForEach(year =>
        {
            var yearProjects = projects.Where(p => p.Year == year).ToList();
            var yearBaseline = baselines.Where(b => b.Year == year).ToList();
            var yearContracts = contracts.Where(c => c.Year == year).ToList();
            months.ForEach(month =>
            {
                var monthInitialPlannedAmount = yearBaseline.FirstOrDefault(b => b.Month == (int) month)?.Amount ?? 0;
                var monthPlannedAmount = yearProjects.FirstOrDefault(p => p.Month == (int) month)?.Amount ?? 0;
                var monthActualAmount = yearContracts.FirstOrDefault(c => c.Month == (int) month)?.Amount ?? 0;
                var dto = new ProjectsSummaryDto
                {
                    Year = year,
                    Month = month,
                    InitialPlannedAmount = monthInitialPlannedAmount,
                    InitialPlannedAmountCumulative = initialPlannedAmountCumulative + monthInitialPlannedAmount,
                    PlannedAmount = monthPlannedAmount,
                    PlannedAmountCumulative = plannedAmountCumulative + monthPlannedAmount,
                    ActualAmount = monthActualAmount,
                    ActualAmountCumulative = actualAmountCumulative + monthActualAmount,
                    ApprovedCost = initiative.ApprovedCosts.Where(a => a.ApprovalDate.Year <= year).Sum(a => a.Amount),
                    InitiativeId = initiative.Id
                };
                result.Add(dto);
                initialPlannedAmountCumulative = dto.InitialPlannedAmountCumulative;
                plannedAmountCumulative = dto.PlannedAmountCumulative;
                actualAmountCumulative = dto.ActualAmountCumulative;
            });
        });

        return result;
    }
}