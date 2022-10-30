﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Persistence.Interceptors;

namespace Moasher.Persistence;

public static class Startup
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<AuditingInterceptor>();
        services.AddDbContext<MoasherDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(MoasherDbContext).Assembly.FullName);
                sqlOptions.UseHierarchyId();
            });
        });
        services.AddScoped<IMoasherDbContext>(sp => sp.GetRequiredService<MoasherDbContext>());
        //services.AddScoped<MoasherDbContextInitializer>();

        var identityBuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
        identityBuilder.AddEntityFrameworkStores<MoasherDbContext>();
    }
}