using IdentityServer4.ResponseHandling;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.IdentityServer.Configurations;
using Moasher.Authentication.Core.Persistence;

namespace Moasher.Authentication.Core.IdentityServer;

internal static class Startup
{
    internal static void AddIdentityServer(this IServiceCollection services, IConfiguration config,
        IWebHostEnvironment env)
    {
        var identityServerBuilder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                //options.EmitStaticAudienceClaim = true;

                options.Authentication.CookieSlidingExpiration = false;
                options.Discovery.ShowApiScopes = false;
                options.Discovery.ShowClaims = true;
            })
            .AddAspNetIdentity<User>();

        identityServerBuilder.AddIdentityServerInPersistence(config);
        if (env.IsDevelopment())
        {
            identityServerBuilder
                .AddDeveloperSigningCredential()
                .AddIdentityServerInMemory();
        }
        else
        {
            // Add Certification
            identityServerBuilder.AddIdentityServerInPersistence(config);
        }

        services.AddScoped<IAuthorizeInteractionResponseGenerator, AppAuthorizeInteractionResponseGenerator>();
        services.AddScoped<ICustomAuthorizeRequestValidator, AppAuthorizeRequestValidator>();
    }
    
    private static void AddIdentityServerInMemory(this IIdentityServerBuilder builder)
    {
        builder
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryPersistedGrants();
    }
    
    private static void AddIdentityServerInPersistence(this IIdentityServerBuilder builder, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var migrationAssembly = typeof(ApplicationDbContext).Assembly.FullName;
        builder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(migrationAssembly));
                options.DefaultSchema = "ids";
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(migrationAssembly));
                options.DefaultSchema = "ids";
            })
            .AddConfigurationStoreCache();
    }
}