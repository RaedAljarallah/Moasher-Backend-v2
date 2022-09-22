using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Deliverables.Commands;

public abstract class DeliverableCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : DeliverableCommandBase
{
    public DeliverableCommandValidatorBase()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم المخرج"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم المخرج"));
        
        RuleFor(command => command.PlannedFinish)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ إنتهاء المخرج المخطط"));
    }
}