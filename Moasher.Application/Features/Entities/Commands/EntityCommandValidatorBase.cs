using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Entities.Commands;

public abstract class EntityCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : EntityCommandBase
{
    protected EntityCommandValidatorBase()
    {
        RuleFor(command => command.Code)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رمز الجهة"))
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("رمز الجهة"));

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم الجهة"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم الجهة"));
    }
}