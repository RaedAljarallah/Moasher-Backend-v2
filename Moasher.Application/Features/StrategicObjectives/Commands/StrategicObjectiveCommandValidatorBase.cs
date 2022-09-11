using FluentValidation;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Services;

namespace Moasher.Application.Features.StrategicObjectives.Commands;

public class StrategicObjectiveCommandValidatorBase<TCommand> : ValidatorBase<TCommand>
    where TCommand : StrategicObjectiveCommandBase
{
    public StrategicObjectiveCommandValidatorBase()
    {
        RuleFor(command => command.Code)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("رمز الهدف الإستراتيجي"))
            .MaximumLength(50).WithMessage(ValidationErrorMessages.MaximumLength("رمز الهدف الإستراتيجي"));

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("اسم الهدف الإستراتيجي"))
            .MaximumLength(255).WithMessage(ValidationErrorMessages.MaximumLength("اسم الهدف الإستراتيجي"));

        RuleFor(command => command.Level)
            .Must((level) => level is >= 1 and <= 4)
            .WithMessage("المستوى يجب أن يكون بين 1-4")
            .OverridePropertyName("");

        RuleFor(command => command.HierarchyId)
            .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("المستوى"))
            .Must(HierarchyIdService.IsValidHierarchyId).WithMessage(ValidationErrorMessages.WrongFormat("المستوى"))
            .OverridePropertyName("")
            .DependentRules(() =>
            {
                RuleFor(command => command)
                    .Must(command => HierarchyIdService.Parse(command.HierarchyId).GetLevel() == command.Level)
                    .WithMessage((command) => GetLevelCodeExampleMessage(command.HierarchyId, command.Level))
                    .OverridePropertyName("");
            });
    }

    private static string GetLevelCodeExampleMessage(string id, short level)
    {
        var codeExample = "1";
        for (var i = 1; i < level; i++)
        {
            codeExample += ".1";
        }

        return $"عدد خانات رمز الهدف من المستوى {level} يجب أن تكون بعدد {level}, مثال: ({codeExample})";
    }
}