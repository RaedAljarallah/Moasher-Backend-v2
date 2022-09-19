using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Milestones.Commands;

public abstract class MilestoneCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : MilestoneCommandBase
{
    protected MilestoneCommandValidatorBase()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم المعلم"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم المعلم"));

        RuleFor(command => command.PlannedFinish)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ إنتهاء المعلم المخطط"));

        RuleFor(command => command.Weight)
            .Must(weight => weight is > 0 and <= 100)
            .WithMessage("وزن المعلم يجب أن يكون بين [0-100]");
    }
}