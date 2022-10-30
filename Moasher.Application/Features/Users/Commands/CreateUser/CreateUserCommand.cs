using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Users.Commands.Common;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand : UserCommandBase, IRequest<UserDto>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IMoasherDbContext context, IMapper mapper,
        IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isValidRole = await _identityService.RoleExistsAsync(request.Role, cancellationToken);
        if (!isValidRole)
        {
            throw new ValidationException(nameof(User.Role), UserValidationMessages.WrongRole);
        }

        var isUserExists = await _identityService.UserExistsAsync(request.Email, cancellationToken);
        if (isUserExists)
        {
            throw new ValidationException(nameof(User.Email), UserValidationMessages.Duplicated);
        }

        var entity = await _context.Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.EntityId, cancellationToken);

        if (entity is null)
        {
            throw new ValidationException(nameof(User.EntityId), UserValidationMessages.WrongEntity);
        }

        var isOrganizationRole = AppRoles.IsOrganizationRole(request.Role);
        if (isOrganizationRole != entity.IsOrganizer)
        {
            throw new ValidationException(nameof(User.Role), UserValidationMessages.WrongRoleSelection);
        }

        var user = _mapper.Map<User>(request);
        user.EmailConfirmed = true;
        user.MustChangePassword = true;
        user.UserName = request.Email;
        
        var createdUser = await _identityService.CreateUserAsync(user, request.Role, cancellationToken);
        createdUser.Entity = entity;
        return _mapper.Map<UserDto>(createdUser);
    }
}