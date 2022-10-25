using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.EnumTypes.Commands;

public abstract class EnumTypeCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : EnumTypeCommandBase
{
    public EnumTypeCommandValidatorBase()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("الاسم"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("الاسم"));
        
        RuleFor(command => command.Style)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اللون"))
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("اللون"));
        
        RuleFor(command => command.Category)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("الفئة"))
            .IsInEnum().WithMessage(ValidationErrorMessages.WrongFormat("الفئة"));
        
        When(command => command.Category == EnumTypeCategory.InitiativeStatus && command.IsDefault is false, () =>
        {
            RuleFor(command => command.LimitFrom)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("فرق نسبة التقدم"))
                .InclusiveBetween(0, 100)
                .WithMessage("فرق نسبة التقدم يجب أن يكون بين 0 - 100");
            
            RuleFor(command => command.LimitTo)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("فرق نسبة التقدم"))
                .InclusiveBetween(0, 100)
                .WithMessage("فرق نسبة التقدم يجب أن يكون بين 0 - 100");
        });
    }
}