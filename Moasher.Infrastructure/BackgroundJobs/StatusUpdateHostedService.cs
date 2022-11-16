using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Extensions;
using Moasher.Domain.Enums;

namespace Moasher.Infrastructure.BackgroundJobs;

public class StatusUpdateHostedService : BackgroundService
{
    private readonly ILogger<StatusUpdateHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private TimeSpan _updateInterval;

    public StatusUpdateHostedService(ILogger<StatusUpdateHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var currentHour = DateTimeOffset.UtcNow.AddHours(3).Hour;
        var remainingHours = currentHour == 0 ? 0 : 24 - currentHour;
        _updateInterval = TimeSpan.FromHours(remainingHours);
        _logger.LogInformation($"{nameof(StatusUpdateHostedService)} is running.");
        _logger.LogInformation("{StatusUpdateHostedServiceName} next run will be after {RemainingHours} hours",
            nameof(StatusUpdateHostedService), remainingHours);
        await UpdateStatus(stoppingToken);
    }

    private async Task UpdateStatus(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            try
            {
                await Task.Delay(_updateInterval, stoppingToken);
                _logger.LogInformation($"{nameof(StatusUpdateHostedService)} is starting");
                using var scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();

                var initiatives = await context.Initiatives
                    .Where(i => i.CalculateStatus)
                    .Include(i => i.Milestones)
                    .AsSplitQuery()
                    .IgnoreQueryFilters()
                    .ToListAsync(stoppingToken);

                var kpis = await context.KPIs
                    .Where(k => k.CalculateStatus)
                    .Include(k => k.Values)
                    .AsSplitQuery()
                    .IgnoreQueryFilters()
                    .ToListAsync(stoppingToken);


                if (initiatives.Any())
                {
                    var statusEnums = await context.EnumTypes
                        .Where(e => e.Category == EnumTypeCategory.InitiativeStatus.ToString())
                        .ToListAsync(stoppingToken);

                    foreach (var initiative in initiatives)
                    {
                        initiative.SetStatus(statusEnums);
                    }
                }

                if (kpis.Any())
                {
                    var statusEnums = await context.EnumTypes
                        .Where(e => e.Category == EnumTypeCategory.KPIStatus.ToString())
                        .ToListAsync(stoppingToken);

                    foreach (var kpi in kpis)
                    {
                        kpi.SetStatus(statusEnums);
                    }
                }

                await context.SaveChangesAsyncFromInternalProcess(stoppingToken);
                _logger.LogInformation($"{nameof(StatusUpdateHostedService)} executed successfully");
                _updateInterval = TimeSpan.FromHours(24);
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