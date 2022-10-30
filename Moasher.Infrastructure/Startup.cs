using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Infrastructure.Authentication.IdentityServer;
using Moasher.Infrastructure.BackgroundJobs;
using Moasher.Infrastructure.Identity;

namespace Moasher.Infrastructure;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity(config);
        //services.AddIdentityServer(config);
        services.AddBackgroundJobs(config);
    }
}