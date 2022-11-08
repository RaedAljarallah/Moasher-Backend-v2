using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Infrastructure.Authentication;
using Moasher.Infrastructure.BackgroundJobs;
using Moasher.Infrastructure.Files;
using Moasher.Infrastructure.Identity;
using Moasher.Infrastructure.Mailing;
using Moasher.Infrastructure.UrlCrypter;

namespace Moasher.Infrastructure;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity(config);
        services.AddAuthentication(config);
        services.AddBackgroundJobs(config);
        services.AddFiles(config);
        services.AddMailing(config);
        services.AddUrlCrypter(config);
    }
}