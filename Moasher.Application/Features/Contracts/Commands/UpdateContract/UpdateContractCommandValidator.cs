using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Contracts.Commands.UpdateContract;

public class UpdateContractCommandValidator : ContractCommandValidatorBase<UpdateContractCommand>
{
    public UpdateContractCommandValidator()
    {
        RuleFor(command => command.Expenditures)
            .Must(expenditures => expenditures.Any())
            .WithMessage(ValidationErrorMessages.NotEmpty("خطة الصرف للعقد"))
            .DependentRules(() =>
            {
                RuleFor(command => command.Expenditures)
                    .Must((command, expenditures) => expenditures.Sum(e => e.PlannedAmount) == command.Amount)
                    .WithMessage("التكاليف الموزعة للخطة غير مطابقة لقيمة العقد");
            });
    }
}