using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.ApprovedCosts.Commands;

public abstract class ApprovedCostCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : ApprovedCostCommandBase
{
    protected ApprovedCostCommandValidatorBase()
    {
        RuleFor(command => command.ApprovalDate)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ اعتماد التكاليف"));
    
        RuleFor(command => command.Amount)
            .InclusiveBetween(0, decimal.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("المبلغ"));
    }
}