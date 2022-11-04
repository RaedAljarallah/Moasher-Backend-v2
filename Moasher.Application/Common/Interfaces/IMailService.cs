using Moasher.Application.Common.Types;

namespace Moasher.Application.Common.Interfaces;

public interface IMailService
{
    public Task SendAsync(MailRequest request, CancellationToken cancellationToken = default);
}