using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Validators;

namespace Moasher.Application.Features.Users.Commands;

public abstract class UserCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : UserCommandBase
{
    protected UserCommandValidatorBase()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("الاسم الأول"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("الاسم الأول"));
        
        RuleFor(command => command.LastName)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("الاسم الأخير"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("الاسم الأخير"));
        
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("البريد الإلكتروني"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("البريد الإلكتروني"))
            .EmailAddress().WithMessage(ValidationErrorMessages.EmailAddress());

        RuleFor(command => command.PhoneNumber)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رقم الهاتف الجوال"))
            .DependentRules(() =>
            {
                RuleFor(command => command.PhoneNumber).PhoneNumber();
            });
        
        RuleFor(command => command.Role)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.NotEmpty("صلاحية المستخدم"));
        
        RuleFor(command => command.EntityId)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationErrorMessages.NotEmpty("الجهة"));
    }
}