using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Programs.BackgroundJobs;

public interface IProgramUpdatedJob : IBackgroundJob
{
}

public class ProgramUpdatedJob : IProgramUpdatedJob
{
    private readonly IServiceProvider _provider;

    public ProgramUpdatedJob(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public async Task ExecuteAsync(params object[] args)
    {
        var programId = (Guid) args.First();
        var cancellationToken = (CancellationToken) args.Last();
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        var program = await context.Programs.FirstOrDefaultAsync(p => p.Id == programId, cancellationToken);
        if (program is not null)
        {
            program.Name = "UpdateName";
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}