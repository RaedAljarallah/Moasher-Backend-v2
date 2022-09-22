using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators.Common.Extensions;

namespace Moasher.Domain.Validators;

public class DeliverableDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _name;
    private readonly DateTimeOffset _plannedFinish;
    private readonly DateTimeOffset? _actualFinish;

    public DeliverableDomainValidator(Initiative initiative, string name, DateTimeOffset plannedFinish,
        DateTimeOffset? actualFinish)
    {
        _initiative = initiative;
        _name = name;
        _plannedFinish = plannedFinish;
        _actualFinish = actualFinish;
    }

    public IDictionary<string, string[]> Validate()
    {
        foreach (var deliverable in _initiative.Deliverables)
        {
            if (string.Equals(deliverable.Name, _name, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(InitiativeDeliverable.Name)] =
                    new[] {DomainValidationErrorMessages.Duplicated("اسم المخرج")};
            }
        }

        _initiative.AchievedAfterInitiativeStart(_plannedFinish, _actualFinish,
            nameof(InitiativeDeliverable.PlannedFinish), nameof(InitiativeDeliverable.ActualFinish), Errors);
        
        _initiative.AchievedBeforeInitiativeFinish(_plannedFinish, _actualFinish,
            nameof(InitiativeDeliverable.PlannedFinish), nameof(InitiativeDeliverable.ActualFinish), Errors);
        
        return Errors;
    }
}