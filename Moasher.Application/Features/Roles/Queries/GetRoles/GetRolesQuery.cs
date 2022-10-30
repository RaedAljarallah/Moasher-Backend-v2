using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Roles.Queries.GetRoles;

public record GetRolesQuery : QueryParameterBase, IRequest<PaginatedList<RoleDto>>;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.Roles
            .AsNoTracking()
            .WithinParameters(new GetRolesQueryParameter(request))
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}