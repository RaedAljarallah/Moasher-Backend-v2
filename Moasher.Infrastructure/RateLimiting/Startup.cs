using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Moasher.Infrastructure.RateLimiting;

internal static class Startup
{
    internal static void AddRateLimiting(this IServiceCollection services, IConfiguration config)
    {
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = false;
            options.StackBlockedRequests = false;
            options.RealIpHeader = "X-Real-IP";
            options.ClientIdHeader = "X-ClientId";
            options.HttpStatusCode = 429;
            options.GeneralRules = new List<RateLimitRule>
            {
                new()
                {
                    Endpoint = "*",
                    Period = "5s",
                    Limit = 10
                }
            };
        });

        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
    
    internal static void UseRateLimiting(this IApplicationBuilder app)
    {
        app.UseIpRateLimiting();
    }
}