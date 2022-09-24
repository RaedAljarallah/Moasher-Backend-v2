using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Issues.Queries.GetIssues;

public record GetIssuesQuery : QueryParameterBase, IRequest<PaginatedList<IssueDto>>
{
    public string? Description { get; set; }
    public string? Scope { get; set; }
    public string? Status { get; set; }
    public string? Impact { get; set; }
    public string? Source { get; set; }
    public string? Reason { get; set; }
    public string? RaisedBy { get; set; }
    public string? InitiativeName { get; set; }
    public DateTimeOffset? RaisedFrom { get; set; }
    public DateTimeOffset? RaisedTo { get; set; }
    public DateTimeOffset? ClosedFrom { get; set; }
    public DateTimeOffset? ClosedTo { get; set; }
    public Guid? ScopeId { get; set; }
    public Guid? StatusId { get; set; }
    public Guid? ImpactId { get; set; }
    public Guid? InitiativeId { get; set; } 
    public Guid? EntityId { get; set; }
}

public class GetIssuesQueryHandler : IRequestHandler<GetIssuesQuery, PaginatedList<IssueDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetIssuesQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<IssueDto>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeIssues.OrderBy(i => i.RaisedAt).ThenBy(i => i.Description)
            .AsNoTracking()
            .WithinParameters(new GetIssuesQueryParameter(request))
            .ProjectTo<IssueDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}