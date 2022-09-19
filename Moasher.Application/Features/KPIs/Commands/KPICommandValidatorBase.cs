using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Validators;

namespace Moasher.Application.Features.KPIs.Commands;

public abstract class KPICommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : KPICommandBase
{
    protected KPICommandValidatorBase()
    {
        RuleFor(command => command.Code)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رمز المؤشر"))
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("رمز المؤشر"));
        
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم المؤشر"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم المؤشر"));
        
        RuleFor(command => command.OwnerName)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم مالك المؤشر"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم مالك المؤشر"));
        
        RuleFor(command => command.OwnerEmail)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("البريد الإلكتروني لمالك المؤشر"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("البريد الإلكتروني لمالك المؤشر"))
            .EmailAddress().WithMessage(ValidationErrorMessages.WrongFormat("البريد الإلكتروني لمالك المؤشر"));

        RuleFor(command => command.OwnerPhoneNumber)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رقم الهاتف لمالك المؤشر"))
            .DependentRules(() =>
            {
                RuleFor(command => command.OwnerPhoneNumber).PhoneNumber();
            });

        RuleFor(command => command.OwnerPosition)
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("منصب مالك المؤشر"));
        
        RuleFor(command => command.Formula)
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("معادلة المؤشر"));
        
        RuleFor(command => command.MeasurementUnit)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("وحدة القياس"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("وحدة القياس"));
        
        RuleFor(command => command.DataSource)
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("مصدر البيانات"));
        
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("وصف المؤشر"))
            .MaximumLength(500).WithMessage(ValidationErrorMessages.MaximumLength("وصف المؤشر"));
        
        RuleFor(command => command.Frequency)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("فترة تكرار القياس"))
            .IsInEnum().WithMessage(ValidationErrorMessages.WrongFormat("فترة تكرار القياس"));

        RuleFor(command => command.Polarity)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("قطبية المؤشر"))
            .IsInEnum().WithMessage(ValidationErrorMessages.WrongFormat("قطبية المؤشر"));

        RuleFor(command => command.ValidationStatus)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("حالة توثيق المؤشر"))
            .IsInEnum().WithMessage(ValidationErrorMessages.WrongFormat("حالة توثيق المؤشر"));
            
        RuleFor(command => command.BaselineValue)
            .InclusiveBetween(float.MinValue, float.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("قيمة خط الأساس"))
            .When(command => command.BaselineValue.HasValue);

        RuleFor(command => command.BaselineYear)
            .InclusiveBetween((short)1980, (short)2080)
            .WithMessage(ValidationErrorMessages.WrongYearRange("سنة قياس خط الأساس"))
            .When(command => command.BaselineYear.HasValue);
        
        When(command => command.CalculateStatus == false, () =>
        {
            RuleFor(command => command.StatusEnumId)
                .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("حالة المؤشر"));
        });
        
        RuleFor(command => command.EntityId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الجهة"));

        RuleFor(command => command.LevelThreeStrategicObjectiveId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الهدف الإستراتيجي (المستوى الثالث)"));
    }
}