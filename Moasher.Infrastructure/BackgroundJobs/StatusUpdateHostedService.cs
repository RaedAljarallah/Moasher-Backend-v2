using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.BackgroundJobs;

public class StatusUpdateHostedService : BackgroundService
{
    private readonly ILogger<StatusUpdateHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public StatusUpdateHostedService(ILogger<StatusUpdateHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await UpdateStatus(stoppingToken);
    }

    private async Task UpdateStatus(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            try
            {
                using var scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
                
            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if stoppingToken was signaled
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred executing status update task");
            }
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(StatusUpdateHostedService)} is stopping.");
        return base.StopAsync(cancellationToken);
    }
}