using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : QueryParameterBase, IRequest<PaginatedList<UserDto>>
{
    private string? _role;

    public string? Role { get => _role; set => _role = value?.Trim(); }

    public Guid? EntityId { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .WithinParameters(new GetUsersQueryParameter(request))
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}