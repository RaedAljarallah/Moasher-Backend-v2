using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Initiatives.Commands;

public abstract class InitiativeCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : InitiativeCommandBase
{
    protected InitiativeCommandValidatorBase()
    {
        RuleFor(command => command.UnifiedCode)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رمز المبادرة الموحد"))
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("رمز المبادرة الموحد"));

        RuleFor(command => command.CodeByProgram)
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("رمز المبادرة في البرنامج"));

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("إسم المبادرة"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("إسم المبادرة"));

        RuleFor(command => command.Scope)
            .MaximumLength(5000).WithMessage(ValidationErrorMessages.MaximumLength("نطاق المبادرة"));

        RuleFor(command => command.TargetSegment)
            .MaximumLength(5000).WithMessage(ValidationErrorMessages.MaximumLength("الشريحة المستهدفة"));

        RuleFor(command => command.ContributionOnStrategicObjective)
            .MaximumLength(5000).WithMessage(ValidationErrorMessages.MaximumLength("المساهمة في الهدف الإستراتيجي"));
        
        RuleFor(command => command.FundStatusEnumId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("حالة التمويل"));
        
        When(command => command.CalculateStatus == false, () =>
        {
            RuleFor(command => command.StatusEnumId)
                .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("حالة التنفيذ"));
        });
        
        RuleFor(command => command.PlannedStart)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ بداية المبادرة المخطط"));
        
        RuleFor(command => command.PlannedFinish)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("تاريخ نهاية المبادرة المخطط"))
            .DependentRules(() =>
            {
                RuleFor(command => command.PlannedFinish)
                    .GreaterThan(command => command.PlannedStart)
                    .WithMessage("تاريخ الإنتهاء المخطط يجب أن يكون بعد تاريخ البداية المخطط");
            });
        
        RuleFor(command => command.ActualStart)
            .LessThan(command => command.PlannedFinish)
            .When(command => command.ActualStart.HasValue)
            .WithMessage("تاريخ البداية الفعلي يجب أن يكون قبل تاريخ الإنتهاء المخطط");

        RuleFor(command => command.ActualFinish)
            .GreaterThan(command => command.ActualStart)
            .When(command => command.ActualFinish.HasValue)
            .WithMessage("تاريخ الإنتهاء الفعلي يجب أن يكون بعد تاريخ البداية الفعلي");
        
        RuleFor(command => command.RequiredCost)
            .InclusiveBetween(0, decimal.MaxValue)
            .WithMessage(ValidationErrorMessages.WrongFormat("التكاليف حسب خطة التنفيذ"));
        
        RuleFor(command => command.CapexCode)
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("رمز المشروع الرأسمالي"));

        RuleFor(command => command.OpexCode)
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("رمز المشروع التشغيلي"));
        
        RuleFor(command => command.EntityId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الجهة"));
        
        RuleFor(command => command.ProgramId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("البرنامج"));
        
        RuleFor(command => command.LevelThreeStrategicObjectiveId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.NotEmpty("الهدف الإستراتيجي (المستوى الثالث)"));
    }
}