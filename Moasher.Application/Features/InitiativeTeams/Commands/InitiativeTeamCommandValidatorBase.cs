using FluentValidation;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Validators;

namespace Moasher.Application.Features.InitiativeTeams.Commands;

public abstract class InitiativeTeamCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : InitiativeTeamCommandBase
{
    protected InitiativeTeamCommandValidatorBase()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("الاسم"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("الاسم"));

        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("البريد الإلكتروني"))
            .EmailAddress().WithMessage(ValidationErrorMessages.EmailAddress());

        RuleFor(command => command.Phone)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رقم الهاتف"))
            .DependentRules(() =>
            {
                RuleFor(command => command.Phone).PhoneNumber();
            });

        RuleFor(command => command.RoleEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الدور"));
        
        RuleFor(command => command.InitiativeId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المبادرة"));
    }
}