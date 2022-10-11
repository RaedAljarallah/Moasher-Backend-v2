using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Projects.Commands;

public abstract class ProjectCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : ProjectCommandBase
{
    protected ProjectCommandValidatorBase()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم المشروع"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم المشروع"));
        
        RuleFor(command => command.PlannedBiddingDate)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ الطرح المخطط"));

        RuleFor(command => command.PlannedContractingDate)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ التعاقد المخطط"))
            .DependentRules(() =>
            {
                RuleFor(command => command.PlannedContractingDate)
                    .GreaterThanOrEqualTo(command => command.PlannedBiddingDate)
                    .WithMessage("تاريخ التعاقد المخطط يجب أن يكون بعد تاريخ الطرح المخطط");
            });

        RuleFor(command => command.EstimatedAmount)
            .InclusiveBetween(0, decimal.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("القيمة التقديرية"));

        RuleFor(command => command.Expenditures)
            .Must(expenditures => expenditures.Any())
            .WithMessage(ValidationErrorMessages.NotEmpty("خطة الصرف للمشروع"));
            
            
        RuleFor(command => command.PhaseEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المرحلة"));
        
        RuleFor(command => command.InitiativeId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المبادرة"));
    }
}