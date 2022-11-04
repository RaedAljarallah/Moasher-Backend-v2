using System.Text;
using Microsoft.Extensions.Options;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Interfaces;
using RazorEngineCore;

namespace Moasher.Infrastructure.Mailing;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly EmailTemplateSettings _settings;
    
    public EmailTemplateService(IOptions<EmailTemplateSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel)
        where T : EmailModelBase
    {
        var template = GetTemplate(templateName);

        var razorEngine = new RazorEngine();
        var modifiedTemplate = razorEngine.Compile(template);
        
        mailTemplateModel.BaseUrl = _settings.BaseUrl ?? string.Empty;
        mailTemplateModel.FirstLogoUrl = _settings.FirstLogoUrl ?? string.Empty;
        mailTemplateModel.SecondLogoUrl = _settings.SecondLogoUrl ?? string.Empty;
        
        return modifiedTemplate.Run(mailTemplateModel);
    }

    private static string GetTemplate(string templateName)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var tmplFolder = Path.Combine(baseDirectory, "EmailTemplates");
        var filePath = Path.Combine(tmplFolder, $"{templateName}.cshtml");

        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, Encoding.Default);
        var mailText = sr.ReadToEnd();
        sr.Close();

        return mailText;
    }
}