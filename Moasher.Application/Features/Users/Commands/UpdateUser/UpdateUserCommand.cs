using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Users.Commands.Common;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand : UserCommandBase, IRequest<UserDto>
{
    public Guid Id { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IMoasherDbContext context, IMapper mapper,
        IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException();
        }

        if (!string.Equals(request.Email, user.Email, StringComparison.CurrentCultureIgnoreCase))
        {
            var isUserExists = await _identityService.UserExistsAsync(request.Email, cancellationToken);
            if (isUserExists)
            {
                throw new ValidationException(nameof(User.Email), UserValidationMessages.Duplicated);
            }
        }
        
        var isValidRole = await _identityService.RoleExistsAsync(request.Role, cancellationToken);
        if (!isValidRole)
        {
            throw new ValidationException(nameof(User.Role), UserValidationMessages.WrongRole);
        }
        
        var entity = await _context.Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.EntityId, cancellationToken);
        
        if (entity is null)
        {
            throw new ValidationException(nameof(User.EntityId), UserValidationMessages.WrongEntity);
        }

        if (!entity.IsOrganizer && AppRoles.IsOrganizationRole(request.Role))
        {
            throw new ValidationException(nameof(User.Role), UserValidationMessages.WrongRoleSelection);
        }

        if (AppRoles.IsSuperAdminRole(user.Role) && !AppRoles.IsSuperAdminRole(request.Role))
        {
            var superAdminRole = AppRoles.GetSuperAdminRole();
            var superAdminUsers = await _identityService.GetUsersInRoleAsync(superAdminRole, cancellationToken);
            if (superAdminUsers.All(u => u.Id == user.Id))
            {
                throw new ValidationException(nameof(User.Role),
                    $"يجب أن يكون هناك مستخدم واحد على الأقل بصلاحية {AppRoles.GetLocalizedName(superAdminRole)}");
            }
        }
        
        _mapper.Map(request, user);

        var userRole = await _identityService.UpdateUserRoleAsync(user, request.Role, cancellationToken);
        user.Role = userRole;

        var updatedUser = await _identityService.UpdateUserAsync(user, cancellationToken);
        updatedUser.Entity = entity;
        return _mapper.Map<UserDto>(updatedUser);
    }
}