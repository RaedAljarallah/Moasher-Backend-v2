using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Risks.Queries.GetRisks;

public record GetRisksQuery : QueryParameterBase, IRequest<PaginatedList<RiskDto>>
{
    public DateTimeOffset? RaisedFrom { get; set; }
    public DateTimeOffset? RaisedTo { get; set; }
    public Guid? TypeId { get; set; }
    public Guid? PriorityId { get; set; }
    public Guid? ProbabilityId { get; set; }
    public Guid? ImpactId { get; set; }
    public Guid? ScopeId { get; set; }
    public Guid? InitiativeId { get; set; }
    public Guid? EntityId { get; set; }
}

public class GetRisksQueryHandler : IRequestHandler<GetRisksQuery, PaginatedList<RiskDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetRisksQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<RiskDto>> Handle(GetRisksQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeRisks.OrderBy(r => r.RaisedAt).ThenBy(r => r.Description)
            .AsNoTracking()
            .WithinParameters(new GetRisksQueryParameter(request))
            .ProjectTo<RiskDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}