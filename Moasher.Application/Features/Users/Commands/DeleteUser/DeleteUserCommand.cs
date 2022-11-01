using MediatR;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public DeleteUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException();
        }

        if (AppRoles.IsSuperAdminRole(user.Role))
        {
            var superAdminRole = AppRoles.GetSuperAdminRole();
            var superAdminUsers = await _identityService.GetUsersInRoleAsync(superAdminRole, cancellationToken);
            if (superAdminUsers.All(u => u.Id == user.Id))
            {
                throw new ConflictException($"يجب أن يكون هناك مستخدم واحد على الأقل بصلاحية {AppRoles.GetLocalizedName(superAdminRole)}");
            }
        }
        
        await _identityService.DeleteUserAsync(user, cancellationToken);
        
        return Unit.Value;
    }
}