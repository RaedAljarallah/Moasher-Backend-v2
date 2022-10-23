using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.Expenditures.Queries.GetExpenditures;

public record GetExpendituresQuery : IRequest<IEnumerable<ExpenditureDto>>
{
    public Guid InitiativeId { get; set; }
    public ushort? Year { get; set; }
}

public class GetExpendituresQueryHandler : IRequestHandler<GetExpendituresQuery, IEnumerable<ExpenditureDto>>
{
    private readonly IMoasherDbContext _context;

    public GetExpendituresQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExpenditureDto>> Handle(GetExpendituresQuery request,
        CancellationToken cancellationToken)
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
                ProjectExpenditures = initiative.Projects.SelectMany(p => p.Expenditures
                        .Where(e => e.Approved)
                        .Where(e => !request.Year.HasValue || (request.Year.HasValue && e.Year == request.Year)))
                    .ToList(),
                ProjectExpendituresBaseline = initiative.Projects.SelectMany(p => p.ExpendituresBaseline
                        .Where(e => e.Approved)
                        .Where(e => !request.Year.HasValue || (request.Year.HasValue && e.Year == request.Year)))
                    .ToList(),
                ContractExpenditures = initiative.Contracts.SelectMany(p => p.Expenditures
                        .Where(e => e.Approved)
                        .Where(e => !request.Year.HasValue || (request.Year.HasValue && e.Year == request.Year)))
                    .ToList(),
                ContractExpendituresBaseline = initiative.Contracts.SelectMany(p => p.ExpendituresBaseline
                        .Where(e => e.Approved)
                        .Where(e => !request.Year.HasValue || (request.Year.HasValue && e.Year == request.Year)))
                    .ToList(),
                Budgets = initiative.Budgets
                    .Where(b => b.Approved)
                    .Where(b => !request.Year.HasValue ||
                                (request.Year.HasValue && b.ApprovalDate.Year == request.Year))
                    .ToList()
            })
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == request.InitiativeId, cancellationToken);

        if (initiative is null)
        {
            throw new NotFoundException();
        }

        var expenditures =
            new List<InitiativeExpenditure>(
                initiative.ProjectExpenditures.Count + initiative.ContractExpenditures.Count);

        expenditures.AddRange(initiative.ProjectExpenditures);
        expenditures.AddRange(initiative.ContractExpenditures);

        var concatExpenditures = expenditures
            .GroupBy(e => new
            {
                e.Year,
                e.Month
            })
            .OrderBy(e => e.Key.Year)
            .ThenBy(e => e.Key.Month)
            .Select(expenditure => new
            {
                expenditure.Key.Year,
                expenditure.Key.Month,
                PlannedAmount = expenditure.Sum(e => e.PlannedAmount),
                ActualAmount = expenditure.Sum(e => e.ActualAmount ?? 0)
            }).ToList();

        var expendituresBaseline = new List<InitiativeExpenditureBaseline>(
            initiative.ProjectExpendituresBaseline.Count + initiative.ContractExpendituresBaseline.Count);

        expendituresBaseline.AddRange(initiative.ProjectExpendituresBaseline);
        expendituresBaseline.AddRange(initiative.ContractExpendituresBaseline);

        var concatExpendituresBaseline = expendituresBaseline
            .GroupBy(e => new
            {
                e.Year,
                e.Month
            })
            .OrderBy(e => e.Key.Year)
            .ThenBy(e => e.Key.Month)
            .Select(baseline => new
            {
                baseline.Key.Year,
                baseline.Key.Month,
                InitialPlannedAmount = baseline.Sum(b => b.InitialPlannedAmount)
            }).ToList();

        var result = new List<ExpenditureDto>();
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
            var yearExpenditures = concatExpenditures.Where(e => e.Year == range.Year).ToList();
            var yearExpendituresBaseline = concatExpendituresBaseline.Where(b => b.Year == range.Year).ToList();
            range.Months.ToList().ForEach(rangeMonth =>
            {
                var month = (Month) rangeMonth;
                var monthInitialPlannedAmount =
                    yearExpendituresBaseline.FirstOrDefault(b => b.Month == month)?.InitialPlannedAmount ?? 0;

                var monthExpenditures = yearExpenditures.FirstOrDefault(e => e.Month == month);
                var monthPlannedAmount = monthExpenditures?.PlannedAmount ?? 0;
                var monthActualAmount = monthExpenditures?.ActualAmount ?? 0;
                var dto = new ExpenditureDto
                {
                    Year = range.Year,
                    Month = month,
                    InitialPlannedAmount = monthInitialPlannedAmount,
                    InitialPlannedAmountCumulative = initialPlannedAmountCumulative + monthInitialPlannedAmount,
                    PlannedAmount = monthPlannedAmount,
                    PlannedAmountCumulative = plannedAmountCumulative + monthPlannedAmount,
                    ActualAmount = monthActualAmount,
                    ActualAmountCumulative = actualAmountCumulative + monthActualAmount,
                    Budget = initiative.Budgets.Where(b => b.ApprovalDate.Year == range.Year).Sum(b => b.Amount),
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