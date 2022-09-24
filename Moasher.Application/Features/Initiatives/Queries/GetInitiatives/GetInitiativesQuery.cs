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
    public string? UnifiedCode { get; set; }
    public string? CodeByProgram { get; set; }
    public string? EntityName { get; set; }
    public string? PortfolioName { get; set; }
    public string? ProgramName { get; set; }
    public string? Status { get; set; }
    public string? FundStatus { get; set; }
    public string? L1Name { get; set; }
    public string? L2Name { get; set; }
    public string? L3Name { get; set; }
    public string? L4Name { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? PortfolioId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? L1Id { get; set; }
    public Guid? L2Id { get; set; }
    public Guid? L3Id { get; set; }
    public Guid? L4Id { get; set; }
    public Guid? StatusId { get; set; }
    public Guid? FundStatusId { get; set; }
    public Guid? IssueStatusId { get; set; }
    public Guid? RiskImpactId { get; set; }
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
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}