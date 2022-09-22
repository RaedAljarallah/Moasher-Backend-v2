using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Budgets.Commands;

public abstract class BudgetCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : BudgetCommandBase
{
    protected BudgetCommandValidatorBase()
    {
        RuleFor(command => command.ApprovalDate)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ اعتماد الميزانية"));
    
        RuleFor(command => command.Amount)
            .InclusiveBetween(0, decimal.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("المبلغ"));
    }
}