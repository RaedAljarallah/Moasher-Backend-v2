using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Users.Queries.EditUser;

public record EditUserQuery : IRequest<EditUserDto>
{
    public Guid Id { get; set; }
}

public class EditUserQueryHandler : IRequestHandler<EditUserQuery, EditUserDto>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public EditUserQueryHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }
    
    public async Task<EditUserDto> Handle(EditUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.Users
            .AsNoTracking()
            .Include(u => u.Entity)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<EditUserDto>(user);
    }
}