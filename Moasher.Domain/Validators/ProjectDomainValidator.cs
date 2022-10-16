using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators.Common.Extensions;

namespace Moasher.Domain.Validators;

public class ProjectDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _name;
    private readonly DateTimeOffset _plannedBidding;
    private readonly DateTimeOffset? _actualBidding;
    private readonly DateTimeOffset _plannedContracting;
    private readonly DateTimeOffset _plannedContractEnd;
    private readonly decimal _estimatedAmount;

    public ProjectDomainValidator(Initiative initiative, string name, DateTimeOffset plannedBidding,
        DateTimeOffset? actualBidding, DateTimeOffset plannedContracting, DateTimeOffset plannedContractEnd,
        decimal estimatedAmount)
    {
        _initiative = initiative;
        _name = name;
        _plannedBidding = plannedBidding;
        _actualBidding = actualBidding;
        _plannedContracting = plannedContracting;
        _plannedContractEnd = plannedContractEnd;
        _estimatedAmount = estimatedAmount;
    }

    public IDictionary<string, string[]> Validate()
    {
        foreach (var project in _initiative.Projects)
        {
            if (string.Equals(project.Name, _name, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(InitiativeProject.Name)] =
                    new[] {DomainValidationErrorMessages.Duplicated("اسم المشروع")};
            }
        }

        _initiative.BiddingAfterInitiativeStart(_plannedBidding, _actualBidding,
            nameof(InitiativeProject.PlannedBiddingDate), nameof(InitiativeProject.ActualBiddingDate), Errors);

        _initiative.BiddingBeforeInitiativeFinish(_plannedBidding, _actualBidding,
            nameof(InitiativeProject.PlannedBiddingDate), nameof(InitiativeProject.ActualBiddingDate), Errors);

        _initiative.ContractEndsBeforeInitiativeFinish(_plannedContractEnd,
            nameof(InitiativeProject.PlannedContractEndDate), Errors);

        return Errors;
    }
}