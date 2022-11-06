using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Moasher.Authentication.Core.Persistence;

internal static class Startup
{
    internal static void AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        var migrationAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(migrationAssembly)));
    }
}