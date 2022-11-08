using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Infrastructure.Authentication.IdentityServer;
using Moasher.Infrastructure.Authentication.Authorization;
namespace Moasher.Infrastructure.Authentication;

internal static class Startup
{
    internal static void AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityServer(config);
        services.AddAuthorization(config);
    }
}