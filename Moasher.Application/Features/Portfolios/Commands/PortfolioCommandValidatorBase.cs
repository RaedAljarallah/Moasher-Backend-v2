using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Portfolios.Commands;

public abstract class PortfolioCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : PortfolioCommandBase
{
    protected PortfolioCommandValidatorBase()
    {
        RuleFor(command => command.Code)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رمز المحفظة"))
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("رمز المحفظة"));

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم المحفظة"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم المحفظة"));
    }
}