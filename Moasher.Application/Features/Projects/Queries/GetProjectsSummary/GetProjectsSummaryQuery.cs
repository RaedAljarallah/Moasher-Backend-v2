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
        var startDate = initiative.ActualStart ?? initiative.PlannedStart;
        var endDate = initiative.ActualFinish ?? initiative.PlannedFinish;
        var endOfCurrentYearDate =
            new DateTimeOffset(new DateTime(DateTimeService.Now.Year, 12, 31), TimeSpan.FromHours(3));
        var yearsMonthsRange = DateTimeService
            .GetYearsMonthsRange(startDate, endDate < endOfCurrentYearDate ? endDate : endOfCurrentYearDate).ToList();
        
        var initialPlannedAmountCumulative = 0m;
        var plannedAmountCumulative = 0m;
        var actualAmountCumulative = 0m;
        yearsMonthsRange.ForEach(range =>
        {
            var yearProjects = projects.Where(p => p.Year == range.Year).ToList();
            var yearBaseline = baselines.Where(b => b.Year == range.Year).ToList();
            var yearContracts = contracts.Where(c => c.Year == range.Year).ToList();
            range.Months.ToList().ForEach(month =>
            {
                var monthInitialPlannedAmount = yearBaseline.FirstOrDefault(b => b.Month == month)?.Amount ?? 0;
                var monthPlannedAmount = yearProjects.FirstOrDefault(p => p.Month == month)?.Amount ?? 0;
                var monthActualAmount = yearContracts.FirstOrDefault(c => c.Month == month)?.Amount ?? 0;
                var dto = new ProjectsSummaryDto
                {
                    Year = range.Year,
                    Month = (Month) month,
                    InitialPlannedAmount = monthInitialPlannedAmount,
                    InitialPlannedAmountCumulative = initialPlannedAmountCumulative + monthInitialPlannedAmount,
                    PlannedAmount = monthPlannedAmount,
                    PlannedAmountCumulative = plannedAmountCumulative + monthPlannedAmount,
                    ActualAmount = monthActualAmount,
                    ActualAmountCumulative = actualAmountCumulative + monthActualAmount,
                    ApprovedCost = initiative.ApprovedCosts.Where(a => a.ApprovalDate.Year <= range.Year).Sum(a => a.Amount),
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