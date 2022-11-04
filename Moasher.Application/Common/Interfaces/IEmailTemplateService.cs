using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Common.Interfaces;

public interface IEmailTemplateService
{
    public string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel) where T : EmailModelBase;
}