using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Moasher.Infrastructure.Authentication.Authorization;

internal static class Startup
{
    internal static void AddAuthorization(this IServiceCollection services, IConfiguration config)
    {
        var options = config.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();
        services.AddAuthorization(builder =>
        {
            builder.AddPolicy("ApiScope", policy =>
            {
                policy.RequireClaim("scope", options.Scope);
            });
        });
        
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }
}