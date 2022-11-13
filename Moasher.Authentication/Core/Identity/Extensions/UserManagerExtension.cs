using Microsoft.AspNetCore.Identity;
using Moasher.Authentication.Core.Identity.Constants;
using Moasher.Authentication.Core.Identity.Entities;

namespace Moasher.Authentication.Core.Identity.Extensions;

public static class UserManagerExtension
{
    public static Task<string> GeneratePasswordChangingToken(this UserManager<User> userManager, User user)
    {
        return userManager.GenerateUserTokenAsync(user, IdentityTokenProviders.PasswordChanging, IdentityTokenPurposes.PasswordChanging);
    }

    public static Task<bool> VerifyPasswordChangingToken(this UserManager<User> userManager, User user, string token)
    {
        return userManager.VerifyUserTokenAsync(user, IdentityTokenProviders.PasswordChanging,
            IdentityTokenPurposes.PasswordChanging, token);
    }

    public static Task<bool> VerifyPasswordResetTokenAsync(this UserManager<User> userManager, User user, string token)
    {
        return userManager.VerifyUserTokenAsync(user, IdentityTokenProviders.PasswordReset,
            UserManager<User>.ResetPasswordTokenPurpose, token);
    }
    
    public static Task<string> GenerateActivationToken(this UserManager<User> userManager, User user)
    {
        return userManager.GenerateUserTokenAsync(user, IdentityTokenProviders.Activation,
            IdentityTokenPurposes.Activation);
    }
    public static Task<bool> VerifyActivationToken(this UserManager<User> userManager, User user, string token)
    {
        return userManager.VerifyUserTokenAsync(user, IdentityTokenProviders.Activation,
            IdentityTokenPurposes.Activation, token);
    }

    public static async Task<string> GetRoleAsync(this UserManager<User> userManager, User user)
    {
        var userRoles = await userManager.GetRolesAsync(user);
        return userRoles.Any() ? userRoles.First().ToUpper() : string.Empty;
    }
}