using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Programs.Commands;

public abstract class ProgramCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : ProgramCommandBase
{
    protected ProgramCommandValidatorBase()
    {
        RuleFor(command => command.Code)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رمز البرنامج"))
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("رمز البرنامج"));

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم البرنامج"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم البرنامج"));
    }
}