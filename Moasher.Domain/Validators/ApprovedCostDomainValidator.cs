using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators.Common.Extensions;

namespace Moasher.Domain.Validators;

public class ApprovedCostDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly DateTimeOffset _approvalDate;
    private readonly decimal _amount;

    public ApprovedCostDomainValidator(Initiative initiative, DateTimeOffset approvalDate, decimal amount)
    {
        _initiative = initiative;
        _approvalDate = approvalDate;
        _amount = amount;
    }

    public IDictionary<string, string[]> Validate()
    {
        foreach (var approvedCost in _initiative.ApprovedCosts)
        {
            if (approvedCost.ApprovalDate == _approvalDate)
            {
                Errors[nameof(InitiativeApprovedCost.ApprovalDate)] = new[]
                    {DomainValidationErrorMessages.Duplicated("تاريخ اعتماد التكاليف")};
            }
        }

        var amountSum = _initiative.ApprovedCosts.Sum(c => c.Amount);
        if (amountSum + _amount > _initiative.RequiredCost)
        {
            Errors[nameof(InitiativeApprovedCost.Amount)] = new[]
            {
                $"مجموع قيمة التكاليف المعتمدة المدخلة [{(amountSum + _amount):N0}] أعلى من قيمة تكاليف المبادرة المطلوبة [{_initiative.RequiredCost:N0}]"
            };
        }

        _initiative.ApprovedAfterInitiativeStart(_approvalDate, nameof(InitiativeApprovedCost.ApprovalDate), Errors);
        _initiative.ApprovedBeforeInitiativeFinish(_approvalDate, nameof(InitiativeApprovedCost.ApprovalDate), Errors);

        return Errors;
    }
}