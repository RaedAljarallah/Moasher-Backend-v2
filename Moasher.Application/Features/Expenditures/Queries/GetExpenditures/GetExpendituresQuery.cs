using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
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
                    .Where(b => !request.Year.HasValue || (request.Year.HasValue && b.ApprovalDate.Year == request.Year))
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
        var months = Enum.GetValues<Month>().ToList();
        var years = expenditures.Select(e => e.Year)
            .Concat(expendituresBaseline.Select(b => b.Year))
            .Distinct()
            .OrderBy(e => e)
            .ToList();
        var initialPlannedAmountCumulative = 0m;
        var plannedAmountCumulative = 0m;
        var actualAmountCumulative = 0m;
        years.ForEach(year =>
        {
            var yearExpenditures = concatExpenditures.Where(e => e.Year == year).ToList();
            var yearExpendituresBaseline = concatExpendituresBaseline.Where(b => b.Year == year).ToList();
            months.ForEach(month =>
            {
                var monthInitialPlannedAmount =
                    yearExpendituresBaseline.FirstOrDefault(b => b.Month == month)?.InitialPlannedAmount ?? 0;

                var monthExpenditures = yearExpenditures.FirstOrDefault(e => e.Month == month);
                var monthPlannedAmount = monthExpenditures?.PlannedAmount ?? 0;
                var monthActualAmount = monthExpenditures?.ActualAmount ?? 0;
                var dto = new ExpenditureDto
                {
                    Year = year,
                    Month = month,
                    InitialPlannedAmount = monthInitialPlannedAmount,
                    InitialPlannedAmountCumulative = initialPlannedAmountCumulative + monthInitialPlannedAmount,
                    PlannedAmount = monthPlannedAmount,
                    PlannedAmountCumulative = plannedAmountCumulative + monthPlannedAmount,
                    ActualAmount = monthActualAmount,
                    ActualAmountCumulative = actualAmountCumulative + monthActualAmount,
                    Budget = initiative.Budgets.Where(b => b.ApprovalDate.Year == year).Sum(b => b.Amount),
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
