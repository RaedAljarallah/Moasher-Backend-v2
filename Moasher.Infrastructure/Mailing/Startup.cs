using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.Mailing;

internal static class Startup
{
    internal static void AddMailing(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MailSettings>(config.GetSection("MailSettings"));
        services.Configure<EmailTemplateSettings>(config.GetSection("EmailTemplateSettings"));
        services.AddTransient<IEmailTemplateService, EmailTemplateService>();
        services.AddTransient<IMailService, SmtpMailService>();
    }
}