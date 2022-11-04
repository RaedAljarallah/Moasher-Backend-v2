using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Infrastructure.Identity.Configurations;

namespace Moasher.Infrastructure.Identity;

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
        
        services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddRoles<Role>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<ActivationTokenProvider>(IdentityTokenProviders.Activation)
            .AddTokenProvider<PasswordChangingTokenProvider>(IdentityTokenProviders.PasswordChanging)
            .AddTokenProvider<PasswordResetTokenProvider>(IdentityTokenProviders.PasswordReset)
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>();

        services.AddScoped<IIdentityService, IdentityService>();
    }
}