using MediatR;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Users.Commands.ResetUserPassword;

public record ResetUserPasswordCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, Unit>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IMailService _mailService;

    public ResetUserPasswordCommandHandler(IIdentityService identityService, IEmailTemplateService emailTemplateService, IMailService mailService)
    {
        _identityService = identityService;
        _emailTemplateService = emailTemplateService;
        _mailService = mailService;
    }
    
    public async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException();
        }

        var tempPassword = await _identityService.ResetUserPassword(user, cancellationToken);
        await SendChangePasswordEmail(user, tempPassword, cancellationToken);
        return Unit.Value;
    }

    private async Task SendChangePasswordEmail(User user, string tempPassword, CancellationToken cancellationToken)
    {
        var emailModel = new ResetUserPasswordEmailModel(user.GetFullName(), tempPassword);
        var emailTemplate = _emailTemplateService.GenerateEmailTemplate(EmailTemplates.ChangeUserPassword, emailModel);
        var mailRequest = new MailRequest(new List<string> {user.Email}, "تغيير كلمة المرور", emailTemplate);
        await _mailService.SendAsync(mailRequest, cancellationToken);
    }
}