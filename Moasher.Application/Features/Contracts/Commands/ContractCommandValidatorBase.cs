using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Contracts.Commands;

public abstract class ContractCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : ContractCommandBase
{
    protected ContractCommandValidatorBase()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم العقد"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم العقد"));
        
        RuleFor(command => command.StartDate)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ بداية العقد"));

        RuleFor(command => command.EndDate)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ نهاية العقد"))
            .DependentRules(() =>
            {
                RuleFor(command => command.EndDate)
                    .GreaterThanOrEqualTo(command => command.StartDate)
                    .WithMessage("تاريخ نهاية العقد يجب أن يكون بعد تاريخ بداية العقد");
            });
        
        RuleFor(command => command.Amount)
            .InclusiveBetween(0, decimal.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("قيمة العقد"));
        
        RuleFor(command => command.RefNumber)
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("الرقم المرجعي"));
        
        RuleFor(command => command.Supplier)
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("المورد"));
        
        RuleFor(command => command.InitiativeId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المبادرة"));
        
        RuleFor(command => command.ProjectId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("المشروع"));
        
        RuleFor(command => command.StatusEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الحالة"));
    }
}