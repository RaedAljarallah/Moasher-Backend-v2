using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiatives;

public record GetInitiativesQuery : QueryParameterBase, IRequest<PaginatedList<InitiativeDto>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? PCode { get; set; }
    public string? Entity { get; set; }
    public string? Portfolio { get; set; }
    public string? Program { get; set; }
    public string? St { get; set; }
    public string? Fst { get; set; }
    public string? L1 { get; set; }
    public string? L2 { get; set; }
    public string? L3 { get; set; }
    public string? L4 { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? PortfolioId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
    public Guid? StId { get; set; }
    public Guid? FstId { get; set; }
    public Guid? IssueId { get; set; }
    public Guid? RiskId { get; set; }
}

public class GetInitiativesQueryHandler : IRequestHandler<GetInitiativesQuery, PaginatedList<InitiativeDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetInitiativesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<InitiativeDto>> Handle(GetInitiativesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Initiatives
            .AsNoTracking()
            .WithinParameters(new GetInitiativesQueryParameter(request))
            .ProjectTo<InitiativeDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.Pn, request.Ps, cancellationToken);
    }
}