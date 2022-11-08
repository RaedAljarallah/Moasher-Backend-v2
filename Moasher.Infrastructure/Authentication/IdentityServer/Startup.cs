using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Constants;
using Moasher.Infrastructure.Authentication.TokenValidator;

namespace Moasher.Infrastructure.Authentication.IdentityServer;

internal static class Startup
{
    internal static void AddIdentityServer(this IServiceCollection services, IConfiguration config)
    {
        var options = config.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();
        services.AddScoped<ITokenValidator, TokenValidatorService>();
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
                builder.TokenValidationParameters.NameClaimType = AppRegisteredClaimNames.name;
                builder.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        var jti = ctx.SecurityToken.Id;
                        var tokenValidator = ctx.HttpContext.RequestServices.GetRequiredService<ITokenValidator>();
                        var isValid = await tokenValidator.IsValid(jti);
                        if (!isValid)
                        {
                            ctx.Fail("Unauthorized");
                        }
                    }
                };
            });
    }
}