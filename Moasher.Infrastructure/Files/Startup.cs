using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.Files;

internal static class Startup
{
    internal static void AddFiles(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
    }
}