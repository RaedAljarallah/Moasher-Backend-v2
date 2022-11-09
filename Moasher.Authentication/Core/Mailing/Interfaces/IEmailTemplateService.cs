using Moasher.Authentication.Core.Mailing.Abstracts;

namespace Moasher.Authentication.Core.Mailing.Interfaces;

public interface IEmailTemplateService
{
    public string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel) where T : EmailModelBase;
}