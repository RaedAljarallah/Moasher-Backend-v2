using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.EditRequests.Queries.GetEditRequests;

public record GetEditRequestsQuery : QueryParameterBase, IRequest<PaginatedList<EditRequestDto>>
{
    public string? Status { get; set; }
    public string? Type { get; set; }
}

public class GetEditRequestsQueryHandler : IRequestHandler<GetEditRequestsQuery, PaginatedList<EditRequestDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetEditRequestsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<EditRequestDto>> Handle(GetEditRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _context.EditRequests
            .OrderBy(e => e.Status)
            .ThenByDescending(e => e.RequestedAt)
            .AsNoTracking()
            .WithinParameters(new GetEditRequestsQueryParameter(request))
            .Include(e => e.Snapshots)
            .ProjectTo<EditRequestDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}