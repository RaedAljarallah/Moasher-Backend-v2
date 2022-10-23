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
    public Guid? InitiativeId { get; set; }
    public Guid? EntityId { get; set; }
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
        var initiatives = await _context.Initiatives
            .AsNoTracking()
            .Where(i => !request.InitiativeId.HasValue || request.InitiativeId == i.Id)
            .Where(i => !request.EntityId.HasValue || request.EntityId == i.EntityId)
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
            .ToListAsync(cancellationToken);

        if (!initiatives.Any())
        {
            return new List<ExpenditureDto>();
        }

        var expenditures =
            new List<InitiativeExpenditure>(
                initiatives.SelectMany(i => i.ProjectExpenditures).Count() +
                initiatives.SelectMany(i => i.ContractExpenditures).Count());

        expenditures.AddRange(initiatives.SelectMany(i => i.ProjectExpenditures));
        expenditures.AddRange(initiatives.SelectMany(i => i.ContractExpenditures));

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
            initiatives.SelectMany(i => i.ProjectExpendituresBaseline).Count() + 
            initiatives.SelectMany(i => i.ContractExpendituresBaseline).Count());

        expendituresBaseline.AddRange(initiatives.SelectMany(i => i.ProjectExpendituresBaseline));
        expendituresBaseline.AddRange(initiatives.SelectMany(i => i.ContractExpendituresBaseline));

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
        var startDate = initiatives.Min(i => i.ActualStart ?? i.PlannedStart);
        var endDate = initiatives.Max(i => i.ActualFinish ?? i.PlannedFinish);
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
                    Budget = initiatives.SelectMany(i => i.Budgets.Where(b => b.ApprovalDate.Year == range.Year)).Sum(b => b.Amount),
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