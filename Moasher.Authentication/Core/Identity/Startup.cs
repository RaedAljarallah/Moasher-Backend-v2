using Microsoft.AspNetCore.Identity;
using Moasher.Authentication.Core.Identity.Configurations;
using Moasher.Authentication.Core.Identity.Constants;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.Persistence;

namespace Moasher.Authentication.Core.Identity;

internal static class Startup
{
    internal static void AddIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ActivationTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(24);
        });

        services.Configure<PasswordChangingTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(24);
        });

        services.Configure<PasswordResetTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(3);
        });

        services.AddIdentity<User, Role>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddDefaultTokenProviders()
        .AddTokenProvider<ActivationTokenProvider>(IdentityTokenProviders.Activation)
        .AddTokenProvider<PasswordChangingTokenProvider>(IdentityTokenProviders.PasswordChanging)
        .AddTokenProvider<PasswordResetTokenProvider>(IdentityTokenProviders.PasswordReset)
        .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
        .AddClaimsPrincipalFactory<ApplicationClaimsPrincipalFactory>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}