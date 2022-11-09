using Moasher.Authentication.Core.Mailing.Abstracts;

namespace Moasher.Authentication.Core.Mailing.Interfaces;

public interface IMailService
{
    public Task SendAsync(MailRequest request, CancellationToken cancellationToken = default);
}