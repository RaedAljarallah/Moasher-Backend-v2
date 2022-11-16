using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.BackgroundJobs;

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
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                _logger.LogInformation($"{nameof(InvalidTokenCleanupHostedService)} is starting");
                using var scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
                
                var found = int.MaxValue;
                while (found >= 100)
                {
                    var expiredTokens = await context.InvalidTokens
                        .Where(t => t.Expiration < DateTime.UtcNow)
                        .OrderBy(t => t.Jti)
                        .Take(100)
                        .ToListAsync(stoppingToken);

                    found = expiredTokens.Count;

                    if (found > 0)
                    {
                        _logger.LogInformation("Removing {Found} invalid tokens", found);
                        context.InvalidTokens.RemoveRange(expiredTokens);
                        await context.SaveChangesAsyncFromInternalProcess(stoppingToken);
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