using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators.Common.Extensions;

namespace Moasher.Domain.Validators;

public class MilestoneDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _name;
    private readonly float _weight;
    private readonly DateTimeOffset _plannedFinish;
    private readonly DateTimeOffset? _actualFinish;

    public MilestoneDomainValidator(Initiative initiative, string name, float weight, DateTimeOffset plannedFinish,
        DateTimeOffset? actualFinish)
    {
        _initiative = initiative;
        _name = name;
        _weight = weight;
        _plannedFinish = plannedFinish;
        _actualFinish = actualFinish;
    }

    public IDictionary<string, string[]> Validate()
    {
        foreach (var milestone in _initiative.Milestones)
        {
            if (string.Equals(milestone.Name, _name, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(InitiativeMilestone.Name)] = new[] {DomainValidationErrorMessages.Duplicated("اسم المعلم")};
            }
        }

        var weightSum = _initiative.Milestones.Sum(m => m.Weight);
        if (weightSum + _weight > 100)
        {
            Errors[nameof(InitiativeMilestone.Weight)] = new[] {$"مجموع اوزان المعالم يجب أن لا تزيد عن 100 - الوزن المتبقي [{100 - weightSum}]"};
        }

        _initiative.AchievedAfterInitiativeStart(_plannedFinish, _actualFinish,
            nameof(InitiativeDeliverable.PlannedFinish), nameof(InitiativeMilestone.ActualFinish), Errors);
        
        _initiative.AchievedBeforeInitiativeFinish(_plannedFinish, _actualFinish,
            nameof(InitiativeDeliverable.PlannedFinish), nameof(InitiativeMilestone.ActualFinish), Errors);
        
        return Errors;
    }
}

