using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Risks.Commands;

public abstract class RiskCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : RiskCommandBase
{
    protected RiskCommandValidatorBase()
    {
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("وصف الخطر"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("وصف الخطر"));
        
        RuleFor(command => command.ImpactDescription)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("وصف الأثر"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("وصف الأثر"));
        
        RuleFor(command => command.Owner)
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("المسؤول"));
        
        RuleFor(command => command.ResponsePlane)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("خطة الإستجابة"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("خطة الإستجابة"));

        RuleFor(command => command.RaisedBy)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رفع بواسطة"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("رفع بواسطة"));

        RuleFor(command => command.RaisedAt)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ رفع الخطر"));
        
        RuleFor(command => command.TypeEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("النوع"));
        
        RuleFor(command => command.PriorityEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الأولوية"));
        
        RuleFor(command => command.ProbabilityEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الاحتمالية"));
        
        RuleFor(command => command.ImpactEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الأثر"));
        
        RuleFor(command => command.ScopeEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("النطاق"));
        
        RuleFor(command => command.InitiativeId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المبادرة"));
    }
}