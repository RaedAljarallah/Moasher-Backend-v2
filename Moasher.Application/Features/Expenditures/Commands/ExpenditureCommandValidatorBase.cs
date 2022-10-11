using FluentValidation;
using Moasher.Application.Common.Constants;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Expenditures.Commands;

public abstract class ExpenditureCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : ExpenditureCommandBase
{
    protected DateTimeOffset StartDate { get; set; }
    protected DateTimeOffset EndDate { get; set; }
    protected IEnumerable<InitiativeExpenditure> Expenditures { get; set; } = Enumerable.Empty<InitiativeExpenditure>();
    
    protected ExpenditureCommandValidatorBase()
    {
        RuleFor(command => command.Year)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("السنة"))
            .DependentRules(() =>
            {
                RuleFor(command => command.Year)
                    .Must(year => year >= StartDate.Year)
                    .WithMessage("سنة الصرف يجب أن تكون أكبر أو تساوي سنة توقيع العقد");

                RuleFor(command => command.Year)
                    .Must(year => year <= EndDate.Year)
                    .WithMessage("سنة الصرف يجب أن تكون أقل أو تساوي سنة نهاية العقد");
            });

        RuleFor(command => command.Month)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("الشهر"))
            .IsInEnum().WithMessage(ValidationErrorMessages.WrongFormat("الشهر"));
        
        RuleFor(command => command.Month)
            .Must((command, month) => !Expenditures.Any(e => e.Month == month && e.Year == command.Year))
            .WithMessage((command, month) => ValidationErrorMessages.Duplicated($"منصرف شهر {month} لسنة {command.Year}"));
        
        RuleFor(command => command.Month)
            .Must((command, month) => new DateTime(command.Year, (int)month, 1) >= new DateTime(StartDate.Year, StartDate.Month, 1))
            .WithMessage($"بداية الصرف يجب أن لا تقل عن شهر {StartDate.Month} لسنة {StartDate.Year}");
        
        RuleFor(command => command.Month)
            .Must((command, month) => new DateTime(command.Year, (int)month, 1) <= new DateTime(EndDate.Year, EndDate.Month, 1))
            .WithMessage($"بداية الصرف يجب أن لا تزيد عن شهر {EndDate.Month} لسنة {EndDate.Year}");
    }
}