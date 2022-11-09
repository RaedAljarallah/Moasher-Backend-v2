using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.Authentication.Authorization;

internal static class Startup
{
    internal static void AddAuthorization(this IServiceCollection services, IConfiguration config)
    {
        var options = config.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();

        services.AddCurrentUser();
        services.AddAuthorization(builder =>
        {
            builder.AddPolicy("ApiScope", policy =>
            {
                policy.RequireClaim("scope", options.Scope);
            });
            
            builder.AddPolicy("SuperAdminAccess", policy =>
            {
                policy.RequireAssertion(ctx => ctx.User.IsSuperAdmin());
            });
        });
        
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }

    internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
    {
        app.UseMiddleware<CurrentUserMiddleware>();
        return app;
    }
    
    private static void AddCurrentUser(this IServiceCollection services)
    {
        services.AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUserService>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());
    }
}