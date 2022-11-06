using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
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

    public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        return _userManager.GetUsersInRoleAsync(roleName);
    }

    public Task<bool> UserExistsAsync(string userEmail, CancellationToken cancellationToken)
    {
        return _userManager.Users.AnyAsync(u => u.NormalizedEmail == _userManager.NormalizeEmail(userEmail),
            cancellationToken);
    }

    public async Task<User> CreateUserAsync(User user, string password, string role, CancellationToken cancellationToken)
    {
        user.Role = _roleManager.NormalizeKey(role);
        user.EmailConfirmed = false;
        user.MustChangePassword = true;
        user.UserName = user.Email;
        var createUserResult = await _userManager.CreateAsync(user, password);
        if (!createUserResult.Succeeded)
        {
            ThrowValidationError(createUserResult.Errors);
        }

        await AddToRole(user, role, cancellationToken);
        return user;
    }
    
    public async Task<User?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        user.UserName = user.Email;
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

    public async Task<string> UpdateUserRoleAsync(User user, string newRole, CancellationToken cancellationToken = default)
    {
        var userCurrentRole = (await _userManager.GetRolesAsync(user)).First();
        if (!string.Equals(newRole, userCurrentRole, StringComparison.CurrentCultureIgnoreCase))
        {
            var removeCurrentRoleResult = await _userManager.RemoveFromRoleAsync(user, userCurrentRole);
            if (!removeCurrentRoleResult.Succeeded)
            {
                ThrowValidationError(removeCurrentRoleResult.Errors);
            }

            await AddToRole(user, newRole, cancellationToken);
        }

        return _roleManager.NormalizeKey(newRole);
    }

    public async Task<string> ResetUserPassword(User user, CancellationToken cancellationToken = default)
    {
        var tempPassword = _userManager.PasswordHasher.HashPassword(user, _userManager.GeneratePassword());
        user.PasswordHash = tempPassword;
        user.MustChangePassword = true;
        await UpdateUserAsync(user, cancellationToken);
        return tempPassword;
    }

    public Task<string> GeneratePassword(CancellationToken cancellationToken = default)
    {
        return Task.Factory.StartNew(() => _userManager.GeneratePassword(), cancellationToken);
    }

    public Task<string> GenerateActivationToken(User user, CancellationToken cancellationToken = default)
    {
        return _userManager.GenerateUserTokenAsync(user, IdentityTokenProviders.Activation, IdentityTokenPurposes.Activation);
    }

    public Task<bool> VerifyActivationToken(User user, string token)
    {
        return _userManager.VerifyUserTokenAsync(user, IdentityTokenProviders.Activation,
            IdentityTokenPurposes.Activation, token);
    }

    public IQueryable<Role> Roles => _roleManager.Roles;
    public IQueryable<User> Users => _userManager.Users;

    private async Task AddToRole(User user, string role, CancellationToken cancellationToken)
    {
        var addToRoleResult = await _userManager.AddToRoleAsync(user, role);
        if (!addToRoleResult.Succeeded)
        {
            await DeleteUserAsync(user, cancellationToken);
            ThrowValidationError(addToRoleResult.Errors);
        }
    }
    
    private static void ThrowValidationError(IEnumerable<IdentityError> errors)
    {
        throw new ValidationException(errors.ToValidationErrors());
    }
}