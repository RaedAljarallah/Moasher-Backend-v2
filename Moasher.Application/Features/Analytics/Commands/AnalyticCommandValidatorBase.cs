using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Analytics.Commands;

public abstract class AnalyticCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : AnalyticCommandBase
{
    protected AnalyticCommandValidatorBase()
    {
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("التحليل"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("التحليل"));
        
        RuleFor(command => command.AnalyzedBy)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("حلل بواسطة"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("حلل بواسطة"));

        RuleFor(command => command.AnalyzedAt)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("حلل بتاريخ"));
        
        RuleFor(command => command.InitiativeId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المبادرة المرتبطة"))
            .OverridePropertyName(string.Empty)
            .When(command => command.KPIId == Guid.Empty);
        
        RuleFor(command => command.KPIId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("مؤشر الأداء المرتبط"))
            .OverridePropertyName(string.Empty)
            .When(command => command.InitiativeId == Guid.Empty);
    }
}