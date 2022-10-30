using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Infrastructure.Identity.Extensions;

namespace Moasher.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public IdentityService(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    
    public Task<bool> RoleExistsAsync(string roleName, CancellationToken cancellationToken)
    {
        return _roleManager.RoleExistsAsync(roleName);
    }

    public Task<bool> UserExistsAsync(string userEmail, CancellationToken cancellationToken)
    {
        return _userManager.Users.AnyAsync(u => u.NormalizedEmail == _userManager.NormalizeEmail(userEmail),
            cancellationToken);
    }

    public async Task<User> CreateUserAsync(User user, string role, CancellationToken cancellationToken)
    {
        user.Role = _roleManager.NormalizeKey(role);
        var tempPassword = _userManager.GeneratePassword();
        var createUserResult = await _userManager.CreateAsync(user, tempPassword);
        if (!createUserResult.Succeeded)
        {
            ThrowValidationError(createUserResult.Errors);
        }
        
        var addToRoleResult = await _userManager.AddToRoleAsync(user, role);
        if (!addToRoleResult.Succeeded)
        {
            await DeleteUserAsync(user, cancellationToken);
            ThrowValidationError(addToRoleResult.Errors);
        }
        return user;
    }

    public async Task<User?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var updateUserResult = await _userManager.UpdateAsync(user);
        if (!updateUserResult.Succeeded)
        {
            ThrowValidationError(updateUserResult.Errors);
        }

        return user;
    }

    public async Task DeleteUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var deleteUserResult = await _userManager.DeleteAsync(user);
        if (!deleteUserResult.Succeeded)
        {
            ThrowValidationError(deleteUserResult.Errors);
        }
    }

    public async Task<bool> UpdateUserSuspensionStatusAsync(User user, bool status, CancellationToken cancellationToken = default)
    {
        user.Suspended = status;
        var updatesUser = await UpdateUserAsync(user, cancellationToken);
        return updatesUser.Suspended;
    }

    public IQueryable<Role> Roles => _roleManager.Roles;
    public IQueryable<User> Users => _userManager.Users;

    private static void ThrowValidationError(IEnumerable<IdentityError> errors)
    {
        throw new ValidationException(errors.ToValidationErrors());
    }
}