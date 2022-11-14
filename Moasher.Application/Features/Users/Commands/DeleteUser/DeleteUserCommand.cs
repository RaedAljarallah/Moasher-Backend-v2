using MediatR;
using Microsoft.EntityFrameworkCore;
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
    private readonly IMoasherDbContext _context;

    public DeleteUserCommandHandler(IIdentityService identityService, IMoasherDbContext context)
    {
        _identityService = identityService;
        _context = context;
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

        var userNotifications = await _context.UserNotifications
            .Where(n => n.UserId == request.Id)
            .ToListAsync(cancellationToken);
        _context.UserNotifications.RemoveRange(userNotifications);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}