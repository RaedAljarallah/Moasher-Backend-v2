using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.ApprovedCosts.Queries.GetApprovedCosts;

public record GetApprovedCostsQuery : QueryParameterBase, IRequest<PaginatedList<ApprovedCostDto>>
{
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public Guid? InitiativeId { get; set; }
    public Guid? EntityId { get; set; }
}

public class GetApprovedCostsQueryHandler : IRequestHandler<GetApprovedCostsQuery, PaginatedList<ApprovedCostDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetApprovedCostsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ApprovedCostDto>> Handle(GetApprovedCostsQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeApprovedCosts.OrderByDescending(a => a.ApprovalDate)
            .AsNoTracking()
            .WithinParameters(new GetApprovedCostsQueryParameter(request))
            .ProjectTo<ApprovedCostDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.Pn, request.Ps, cancellationToken);
    }
}