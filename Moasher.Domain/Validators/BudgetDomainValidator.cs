using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators.Common.Extensions;

namespace Moasher.Domain.Validators;

public class BudgetDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly DateTimeOffset _approvalDate;
    private readonly decimal _amount;

    public BudgetDomainValidator(Initiative initiative, DateTimeOffset approvalDate, decimal amount)
    {
        _initiative = initiative;
        _approvalDate = approvalDate;
        _amount = amount;
    }
    public IDictionary<string, string[]> Validate()
    {
        foreach (var budget in _initiative.Budgets)
        {
            if (budget.ApprovalDate.Year == _approvalDate.Year)
            {
                Errors[nameof(InitiativeBudget.ApprovalDate)] = new[]
                    {DomainValidationErrorMessages.Duplicated("سنة اعتماد الميزانية")};
            }
        }
        
        var amountSum = _initiative.Budgets.Sum(c => c.Amount);
        if (amountSum + _amount > (_initiative.ApprovedCost ?? 0))
        {
            Errors[nameof(InitiativeBudget.Amount)] = new[]
            {
                $"مجموع قيمة الميزانيات المدخلة [{(amountSum + _amount):N0}] أعلى من قيمة تكاليف المبادرة المعتمدة [{_initiative.ApprovedCost ?? 0:N0}]"
            };
        }
        
        _initiative.ApprovedAfterInitiativeStart(_approvalDate, nameof(InitiativeBudget.ApprovalDate), Errors);
        _initiative.ApprovedBeforeInitiativeFinish(_approvalDate, nameof(InitiativeBudget.ApprovalDate), Errors);
        
        return Errors;
    }
}