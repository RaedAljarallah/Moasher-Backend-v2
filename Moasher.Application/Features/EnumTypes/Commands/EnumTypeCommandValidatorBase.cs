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
        
        When(command => command.Category == EnumTypeCategory.InitiativeStatus, () =>
        {
            RuleFor(command => command.Metadata)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("فرق نسبة التقدم"));
            RuleFor(command => command.Metadata)
                .Must(metadata => metadata.ContainsKey("diff"))
                .WithMessage("رمز فرق نسبة التقدم يجب أن يكون diff")
                .DependentRules(() =>
                {
                    RuleFor(command => command.Metadata)
                        .Must(metadata => float.TryParse(metadata["diff"], out _))
                        .WithMessage(ValidationErrorMessages.WrongFormat("فرق نسبة التقدم"))
                        .DependentRules(() =>
                        {
                            RuleFor(command => float.Parse(command.Metadata["diff"]))
                                .InclusiveBetween(0.1f, 100)
                                .WithName("metadata")
                                .WithMessage("فرق نسبة التقدم يجب أن يكون بين 0.1 - 100");
                        });
                });
        });
    }
}