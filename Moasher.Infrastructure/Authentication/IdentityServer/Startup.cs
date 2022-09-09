using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Constants;

namespace Moasher.Infrastructure.Authentication.IdentityServer;

internal static class Startup
{
    internal static void AddIdentityServer(this IServiceCollection services, IConfiguration config)
    {
        var options = config.GetSection("IdentityServer").Get<IdentityServerOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(builder =>
            {
                builder.Authority = options.Authority;
                builder.Audience = options.Audience;
                builder.MapInboundClaims = false;
                builder.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                builder.TokenValidationParameters.ValidateAudience = true;
                builder.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                builder.TokenValidationParameters.RoleClaimType = AppRegisteredClaimNames.Role;
            });

        services.AddAuthorization(builder =>
        {
            builder.AddPolicy("ApiScope", policy =>
            {
                policy.RequireClaim("scope", options.Scope);
            });
        });
    }
}