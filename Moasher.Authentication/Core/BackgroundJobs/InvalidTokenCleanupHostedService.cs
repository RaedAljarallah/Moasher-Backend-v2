using Moasher.Authentication.Core.Identity.Services;

namespace Moasher.Authentication.Core.BackgroundJobs;

public class InvalidTokenCleanupHostedService : BackgroundService
{
    private readonly ILogger<InvalidTokenCleanupHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public InvalidTokenCleanupHostedService(ILogger<InvalidTokenCleanupHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(InvalidTokenCleanupHostedService)} is running.");
        await RemoveInvalidTokens(stoppingToken);
    }

    private async Task RemoveInvalidTokens(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            try
            {
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                _logger.LogInformation($"{nameof(InvalidTokenCleanupHostedService)} is starting");
                using var scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var invalidTokenService = scope.ServiceProvider.GetRequiredService<IInvalidToken>();
                
                var found = int.MaxValue;
                while (found >= 100)
                {
                    var expiredTokens = await invalidTokenService.GetExpiredTokens(100, stoppingToken);

                    found = expiredTokens.Count;

                    if (found > 0)
                    {
                        _logger.LogInformation("Removing {Found} invalid tokens", found);
                        await invalidTokenService.RemoveExpiredTokens(expiredTokens, stoppingToken);
                    }
                }
                
                _logger.LogInformation($"{nameof(InvalidTokenCleanupHostedService)} executed successfully");
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
        _logger.LogInformation($"{nameof(InvalidTokenCleanupHostedService)} is stopping.");
        return base.StopAsync(cancellationToken);
    }
}