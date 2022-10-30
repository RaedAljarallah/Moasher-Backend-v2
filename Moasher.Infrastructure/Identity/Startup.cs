using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;

namespace Moasher.Infrastructure.Identity;

internal static class Startup
{
    internal static void AddIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddRoles<Role>();

        services.AddScoped<IIdentityService, IdentityService>();
    }
}