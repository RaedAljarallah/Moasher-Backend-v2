using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Moasher.WebApi.Filters;

namespace Moasher.WebApi;

public static class Startup
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add<ApiExceptionFilterAttribute>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddFluentValidationClientsideAdapters();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{config.GetValue<string>("AuthenticationSettings:Authority")}/connect/authorize"),
                        TokenUrl = new Uri($"{config.GetValue<string>("AuthenticationSettings:Authority")}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            {"access_as_user", "Moasher API - user access"},
                            {"openid", "Default scope for OpenId"},
                            {"profile", "Default scope for Profile"}
                        }
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] {"access_as_user", "openid", "profile"}
                }
            });
        });
        
        services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
        {
            builder
                .WithOrigins(config.GetSection("CorsConfigurations:Origins").Get<string[]>())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders(HeaderNames.ContentDisposition);
        }));
    }
}