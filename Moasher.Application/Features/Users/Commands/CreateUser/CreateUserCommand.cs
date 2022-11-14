using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;
using Moasher.Application.Features.Users.Commands.Common;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand : UserCommandBase, IRequest<UserDto>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IMailService _mailService;
    private readonly IUrlCrypter _urlCrypter;

    public CreateUserCommandHandler(IMoasherDbContext context, IMapper mapper,
        IIdentityService identityService, IEmailTemplateService emailTemplateService, IMailService mailService,
        IUrlCrypter urlCrypter)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
        _emailTemplateService = emailTemplateService;
        _mailService = mailService;
        _urlCrypter = urlCrypter;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUserExists = await _identityService.UserExistsAsync(request.Email, cancellationToken);
        if (isUserExists)
        {
            throw new ValidationException(nameof(User.Email), UserValidationMessages.Duplicated);
        }

        var isValidRole = await _identityService.RoleExistsAsync(request.Role, cancellationToken);
        if (!isValidRole)
        {
            throw new ValidationException(nameof(User.Role), UserValidationMessages.WrongRole);
        }

        var entity = await _context.Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.EntityId, cancellationToken);

        if (entity is null)
        {
            throw new ValidationException(nameof(User.EntityId), UserValidationMessages.WrongEntity);
        }

        if (!entity.IsOrganizer && AppRoles.IsOrganizationRole(request.Role))
        {
            throw new ValidationException(nameof(User.Role), UserValidationMessages.WrongRoleSelection);
        }

        var user = _mapper.Map<User>(request);

        var tempPassword = await _identityService.GeneratePassword(cancellationToken);
        var createdUser = await _identityService.CreateUserAsync(user, tempPassword, request.Role, cancellationToken);
        createdUser.Entity = entity;

        await SendConfirmationEmail(createdUser, tempPassword, cancellationToken);

        return _mapper.Map<UserDto>(createdUser);
    }

    private async Task SendConfirmationEmail(User user, string tempPassword, CancellationToken cancellationToken)
    {
        var changePasswordToken = _urlCrypter.Encode(await _identityService.GenerateActivationToken(user, cancellationToken));
        var userId = _urlCrypter.Encode(user.Id.ToString());
        var emailModel = new CreateUserEmailModel(user.GetFullName(), tempPassword,
            $"accounts/activation?token={changePasswordToken}&id={userId}");
        var emailTemplate = _emailTemplateService.GenerateEmailTemplate(EmailTemplates.UserCreation, emailModel);
        var mailRequest = new MailRequest(new List<string> {user.Email}, "تفعيل الحساب", emailTemplate);
        await _mailService.SendAsync(mailRequest, cancellationToken);
    }
}