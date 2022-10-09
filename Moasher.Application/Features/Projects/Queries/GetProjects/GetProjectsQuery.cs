using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Projects.Queries.GetProjects;

public record GetProjectsQuery : QueryParameterBase, IRequest<PaginatedList<ProjectDto>>
{
    public string? Name { get; set; }
    public DateTimeOffset? PlannedBiddingFrom { get; set; }
    public DateTimeOffset? PlannedBiddingTo { get; set; }
    public DateTimeOffset? ActualBiddingFrom { get; set; }
    public DateTimeOffset? ActualBiddingTo { get; set; }
    public DateTimeOffset? PlannedContractingFrom { get; set; }
    public DateTimeOffset? PlannedContractingTo { get; set; }
    public Guid? PhaseId { get; set; }
    public string? Status { get; set; }
}

public class GetProjectQueryHandler : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeProjects.OrderBy(p => p.PlannedBiddingDate).ThenBy(p => p.Name)
            .Where(p => !p.Contracted)
            .AsNoTracking()
            .WithinParameters(new GetProjectsQueryParameter(request))
            .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}