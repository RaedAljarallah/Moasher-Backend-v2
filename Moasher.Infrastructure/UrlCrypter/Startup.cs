using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.UrlCrypter;

internal static class Startup
{
    internal static void AddUrlCrypter(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IUrlCrypter, UrlCrypterService>();
    }
}