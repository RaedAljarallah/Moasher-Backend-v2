using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Extensions;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesSummary;

public record GetInitiativesSummaryQuery : IRequest<InitiativeSummaryDto>
{
    public Guid? Id { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? PortfolioId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
    public bool? ForDashboard { get; set; }
}

public class GetInitiativesSummaryQueryHandler : IRequestHandler<GetInitiativesSummaryQuery, InitiativeSummaryDto>
{
    private readonly IMoasherDbContext _context;

    public GetInitiativesSummaryQueryHandler(IMoasherDbContext context)
    {
        _context = context;
    }

    public async Task<InitiativeSummaryDto> Handle(GetInitiativesSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var initiatives = await _context.Initiatives
            .WithinParameters(new GetInitiativesSummaryQueryParameter(request))
            .Include(i => i.Projects.Where(p => p.Approved)).ThenInclude(p => p.Expenditures.Where(e => e.Approved))
            .Include(i => i.Contracts.Where(c => c.Approved)).ThenInclude(c => c.Expenditures.Where(e => e.Approved))
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        var summaryDto = new InitiativeSummaryDto();
        initiatives.ForEach(i =>
        {
            summaryDto.Statuses.Add(i.Status);
            summaryDto.FundStatuses.Add(i.FundStatus);
            summaryDto.ApprovedCost += i.ApprovedCost ?? 0m;
            summaryDto.RequiredCost += i.RequiredCost;
            summaryDto.ContractsAmount += i.ContractsAmount ?? 0m;
            summaryDto.TotalExpenditure += i.TotalExpenditure ?? 0m;
            summaryDto.PlannedToDateExpenditure += i.GetPlannedToDateExpenditure();
            summaryDto.PlannedToDateContractsAmount += i.GetPlannedToDateContractsAmount();
            summaryDto.PlannedProgress += i.PlannedProgress ?? 0f;
            summaryDto.ActualProgress += i.ActualProgress ?? 0f;
            summaryDto.TotalBudget += i.TotalBudget ?? 0m;
            summaryDto.CurrentYearBudget += i.CurrentYearBudget ?? 0m;
            summaryDto.CurrentYearExpenditure += i.CurrentYearExpenditure ?? 0m;
        });

        if (initiatives.Any())
        {
            summaryDto.PlannedProgress = summaryDto.PlannedProgress / (initiatives.Count * 100) * 100;
            summaryDto.ActualProgress = summaryDto.ActualProgress / (initiatives.Count * 100) * 100;

            var earnedValue = (float) summaryDto.RequiredCost * (summaryDto.ActualProgress / 100);
            summaryDto.EstimatedBudgetAtCompletion =
                summaryDto.TotalExpenditure + (summaryDto.RequiredCost - (decimal) earnedValue);
        }

        return summaryDto;
    }
}