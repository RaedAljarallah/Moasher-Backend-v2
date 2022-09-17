using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Behaviours;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Application.Features.Entities.BackgroundJobs;

namespace Moasher.Application;

public static class Startup
{
    public static void AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddBackgroundJobs(Assembly.GetExecutingAssembly());
    }

    private static void AddBackgroundJobs(this IServiceCollection services, Assembly assembly)
    {
        var interfaceTypes = assembly.DefinedTypes
            .Where(t => typeof(IBackgroundJob).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Service = t.GetInterfaces().FirstOrDefault(),
                Implementation = t
            })
            .Where(t => t.Service is not null && typeof(IBackgroundJob).IsAssignableFrom(t.Service));

        foreach (var type in interfaceTypes)
        {
            services.AddSingleton(type.Service!, type.Implementation);
        }
    }
}