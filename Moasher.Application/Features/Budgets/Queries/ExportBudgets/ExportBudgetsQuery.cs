using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Budgets.Queries.ExportBudgets;

public record ExportBudgetsQuery : IRequest<ExportedBudgetsDto>;

public class ExportBudgetsQueryHandler : IRequestHandler<ExportBudgetsQuery, ExportedBudgetsDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportBudgetsQueryHandler(IMoasherDbContext context, IMapper mapper, ICsvFileBuilder csvFileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _csvFileBuilder = csvFileBuilder;
    }
    
    public async Task<ExportedBudgetsDto> Handle(ExportBudgetsQuery request, CancellationToken cancellationToken)
    {
        var budgets = await _context.InitiativeBudgets.OrderByDescending(a => a.ApprovalDate)
            .AsNoTracking()
            .ProjectTo<BudgetDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ExportedBudgetsDto("Budgets.csv", _csvFileBuilder.BuildBudgetsFile(budgets));
    }
}