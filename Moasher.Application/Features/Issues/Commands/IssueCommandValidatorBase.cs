using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Issues.Commands;

public abstract class IssueCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : IssueCommandBase
{
    protected IssueCommandValidatorBase()
    {
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("وصف المعوق"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("وصف المعوق"));
        
        RuleFor(command => command.ImpactDescription)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("وصف الأثر"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("وصف الأثر"));
        
        RuleFor(command => command.Source)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("مصدر المعوق"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("مصدر المعوق"));
        
        RuleFor(command => command.Reason)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("سبب المعوق"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("سبب المعوق"));
        
        RuleFor(command => command.Resolution)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("الحل القترح"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("الحل القترح"));
        
        RuleFor(command => command.EstimatedResolutionDate)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ الحل المتوقع"));

        RuleFor(command => command.RaisedAt)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ رفع المعوق"));

        RuleFor(command => command.RaisedBy)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رفع بواسطة"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("رفع بواسطة"));

        RuleFor(command => command.ScopeEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("النطاق"));
        
        RuleFor(command => command.StatusEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الحالة"));
        
        RuleFor(command => command.ImpactEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الأثر"));
        
        RuleFor(command => command.InitiativeId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المبادرة"));
    }
}