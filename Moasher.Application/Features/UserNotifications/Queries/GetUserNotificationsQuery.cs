using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.UserNotifications.Queries;

public record GetUserNotificationsQuery : QueryParameterBase, IRequest<PaginatedList<UserNotificationDto>>
{
    public bool? Read { get; set; }
}

public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, PaginatedList<UserNotificationDto>>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public GetUserNotificationsQueryHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<UserNotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await _context.UserNotifications
            .OrderByDescending(n => n.HasRead)
            .ThenByDescending(n => n.CreatedAt)
            .AsNoTracking()
            .WithinParameters(new GetUserNotificationsQueryParameter(request))
            .ProjectTo<UserNotificationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
            
    }
}