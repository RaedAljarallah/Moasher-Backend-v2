﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.BackgroundJobs.Common;

public class QueuedHostedService  : BackgroundService
{
    private readonly IBackgroundQueue _queue;
    private readonly ILogger<QueuedHostedService> _logger;

    public QueuedHostedService(IBackgroundQueue queue, ILogger<QueuedHostedService> logger)
    {
        _queue = queue;
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(QueuedHostedService)} is running.");
        return ProcessTaskQueueAsync(stoppingToken);
    }

    private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            try
            {
                var task = await _queue.DequeueAsync(stoppingToken);
                await task(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if stoppingToken was signaled
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred executing queued task");
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(QueuedHostedService)} is stopping.");
        
        await base.StopAsync(cancellationToken);
    }
}