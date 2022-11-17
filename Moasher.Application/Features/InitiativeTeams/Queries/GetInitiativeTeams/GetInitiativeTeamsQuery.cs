using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.InitiativeTeams.Queries.GetInitiativeTeams;

public record GetInitiativeTeamsQuery : QueryParameterBase, IRequest<PaginatedList<InitiativeTeamDto>>
{
    public Guid? RoleId { get; set; }
    public Guid? InitiativeId { get; set; }
}

public class GetInitiativeTeamsQueryHandler : IRequestHandler<GetInitiativeTeamsQuery, PaginatedList<InitiativeTeamDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetInitiativeTeamsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<InitiativeTeamDto>> Handle(GetInitiativeTeamsQuery request, CancellationToken cancellationToken)
    {
        return await _context.InitiativeTeams
            .AsNoTracking()
            .WithinParameters(new GetInitiativeTeamsQueryParameter(request))
            .ProjectTo<InitiativeTeamDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}