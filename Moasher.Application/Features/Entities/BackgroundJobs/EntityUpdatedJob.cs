using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Entities.BackgroundJobs;

public interface IEntityUpdatedJob : IBackgroundJob
{
}

public class EntityUpdatedJob : IEntityUpdatedJob
{
    private readonly IServiceProvider _provider;

    public EntityUpdatedJob(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task ExecuteAsync(params object[] args)
    {
        var entityId = (Guid) args.First();
        var cancellationToken = (CancellationToken) args.Last();
        Thread.Sleep(3000);
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        var entity = await context.Entities.FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken);
        if (entity is not null)
        {
            entity.Name = "BackgroundUpdatedName";
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}