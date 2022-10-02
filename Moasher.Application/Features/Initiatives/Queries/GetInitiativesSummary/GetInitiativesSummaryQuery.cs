using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;

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
    
    public async Task<InitiativeSummaryDto> Handle(GetInitiativesSummaryQuery request, CancellationToken cancellationToken)
    {
        var initiatives = await _context.Initiatives
            .WithinParameters(new GetInitiativesSummaryQueryParameter(request))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var summaryDto = new InitiativeSummaryDto();
        initiatives.ForEach(i =>
        {
            summaryDto.Statuses.Add(i.Status);
            summaryDto.FundStatuses.Add(i.FundStatus);
            summaryDto.PlannedProgress += i.PlannedProgress ?? 0f;
            summaryDto.ActualProgress += i.ActualProgress ?? 0f;
            summaryDto.RequiredCost += i.RequiredCost;
            summaryDto.ApprovedCost += i.ApprovedCost ?? 0m;
            summaryDto.TotalBudget += i.TotalBudget ?? 0m;
            summaryDto.CurrentYearBudget += i.CurrentYearBudget ?? 0m;
            // summaryDto.ContractsAmount += i.ContractsAmount ?? 0m;
            // summaryDto.TotalExpenditure += i.TotalExpenditure ?? 0m;
            // summaryDto.CurrentYearExpenditure += i.CurrentYearExpenditure ?? 0m;
        });
        
        if (initiatives.Any())
        {
            summaryDto.PlannedProgress = summaryDto.PlannedProgress / (initiatives.Count * 100) * 100;
            summaryDto.ActualProgress = summaryDto.ActualProgress / (initiatives.Count * 100) * 100;
        }

        return summaryDto;
    }
}