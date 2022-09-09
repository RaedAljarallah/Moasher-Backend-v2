using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Moasher.WebApi.Filters;

namespace Moasher.WebApi;

public static class Startup
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        services.AddControllers(options =>
        {
            if (!env.IsDevelopment())
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            }
        });

        services.AddFluentValidationClientsideAdapters();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
        {
            builder
                .WithOrigins(config.GetSection("CorsConfigurations:Origins").Get<string[]>())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));
    }
}