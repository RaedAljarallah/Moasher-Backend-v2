using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Users.Commands.Common;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand : UserCommandBase, IRequest<UserDto>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(IMoasherDbContext context, IMapper mapper,
        RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isValidRole = await _roleManager.RoleExistsAsync(request.Role);
        if (!isValidRole)
        {
            throw new ValidationException(nameof(User.Role), UserValidationMessages.WrongRole);
        }

        var isUserExists = await _context.Users
            .AnyAsync(u => u.NormalizedEmail == _userManager.NormalizeEmail(request.Email), cancellationToken);
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

        var tempPassword = _userManager.GeneratePassword();
        var createUserResult = await _userManager.CreateAsync(user, tempPassword);
        if (!createUserResult.Succeeded)
        {
            throw new ValidationException(createUserResult.Errors.ToValidationErrors());
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, request.Role);
        if (!addToRoleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            throw new ValidationException(addToRoleResult.Errors.ToValidationErrors());
        }

        return _mapper.Map<UserDto>(user);
    }
}