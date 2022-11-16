using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Infrastructure.BackgroundJobs.Common;

namespace Moasher.Infrastructure.BackgroundJobs;

internal class BackgroundJobsOptions
{
    public int QueueCapacity { get; set; }
}
internal static class Startup
{
    internal static void AddBackgroundJobs(this IServiceCollection services, IConfiguration config)
    {
        var options = config.GetSection("BackgroundJobs").Get<BackgroundJobsOptions>();
        services.AddSingleton<IBackgroundQueue>(_ => new BackgroundQueue(options.QueueCapacity));
        services.AddHostedService<QueuedHostedService>();
        services.AddHostedService<StatusUpdateHostedService>();
        services.AddHostedService<InvalidTokenCleanupHostedService>();
    }
}