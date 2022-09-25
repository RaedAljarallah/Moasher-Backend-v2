using FluentValidation;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.KPIValues.Commands;

public abstract class KPIValueCommandValidatorBase<TCommand> : AbstractValidator<TCommand>
    where TCommand : KPIValueCommandBase
{
    protected KPIValueCommandValidatorBase()
    {
        RuleFor(command => command.MeasurementPeriod)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("فترة القياس"))
            .IsInEnum().WithMessage(ValidationErrorMessages.WrongFormat("فترة القياس"));
        
        RuleFor(command => command.Year)
            .NotNull().WithMessage(ValidationErrorMessages.NotEmpty("السنة"))
            .InclusiveBetween((short)1980, (short)2080)
            .WithMessage(ValidationErrorMessages.WrongYearRange("السنة"));
        
        RuleFor(command => command.TargetValue)
            .NotNull().WithMessage(ValidationErrorMessages.NotEmpty("القيمة المستهدفة"))
            .InclusiveBetween(float.MinValue, float.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("القيمة المستهدفة"));
        
        RuleFor(command => command.ActualValue)
            .InclusiveBetween(float.MinValue, float.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("القيمة الفعلية"))
            .When(command => command.ActualValue.HasValue);
        
        RuleFor(command => command.ActualValue)
            .NotNull().WithMessage(ValidationErrorMessages.NotEmpty("القيمة الفعلية"))
            .When(command => command.ActualFinish.HasValue);
        
        RuleFor(command => command.PlannedFinish)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ الإنجاز المخطط"))
            .Must((date, command) => date.Year <= command.Year)
            .WithMessage("سنة تاريخ القياس المخطط يجب أن تكون أكبر أو تساوي سنة القيمة");
        
        RuleFor(command => command.ActualFinish)
            .NotNull().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ الإنجاز الفعلي"))
            .When(command => command.ActualValue.HasValue);
        
        RuleFor(command => command.KPIId)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationErrorMessages.NotEmpty("مؤشر الأداء المرتبط"));
    }
}