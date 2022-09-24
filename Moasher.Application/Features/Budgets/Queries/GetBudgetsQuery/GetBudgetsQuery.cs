using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Budgets.Queries.GetBudgetsQuery;

public record GetBudgetsQuery : QueryParameterBase, IRequest<PaginatedList<BudgetDto>>
{
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public Guid? InitiativeId { get; set; }
    public Guid? EntityId { get; set; }
}

public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, PaginatedList<BudgetDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetBudgetsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeBudgets.OrderByDescending(a => a.ApprovalDate)
            .AsNoTracking()
            .WithinParameters(new GetBudgetsQueryParameter(request))
            .ProjectTo<BudgetDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}