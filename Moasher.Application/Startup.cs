using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Behaviours;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.UserNotifications;

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
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EditRequestBehaviour<,>));
        services.AddScoped<IUserNotification, UserNotificationService>();
    }
}