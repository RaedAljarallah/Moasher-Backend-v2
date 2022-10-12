using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Validators.Common.Extensions;

namespace Moasher.Domain.Validators;

public class ContractDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _name;
    private readonly string? _refNumber;
    private readonly decimal _amount;
    private readonly DateTimeOffset _startDate;
    private readonly DateTimeOffset _endDate;

    public ContractDomainValidator(Initiative initiative, string name, string? refNumber, decimal amount,
        DateTimeOffset startDate, DateTimeOffset endDate)
    {
        _initiative = initiative;
        _name = name;
        _refNumber = refNumber;
        _amount = amount;
        _startDate = startDate;
        _endDate = endDate;
    }

    public IDictionary<string, string[]> Validate()
    {
        foreach (var contract in _initiative.Contracts)
        {
            if (string.Equals(contract.Name, _name, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(InitiativeContract.Name)] =
                    new[] {DomainValidationErrorMessages.Duplicated("اسم العقد")};
            }

            if (!string.IsNullOrWhiteSpace(_refNumber) && !string.IsNullOrWhiteSpace(contract.RefNumber))
            {
                if (string.Equals(contract.RefNumber, _refNumber, StringComparison.CurrentCultureIgnoreCase))
                {
                    Errors[nameof(InitiativeContract.RefNumber)] =
                        new[] {DomainValidationErrorMessages.Duplicated("الرقم المرجعي")};
                }
            }
        }

        var amountSum = _initiative.Contracts.Sum(c => c.Amount);
        if (amountSum + _amount > _initiative.ApprovedCost)
        {
            Errors[nameof(InitiativeContract.Amount)] = new[]
            {
                $"مجموع قيمة العقود المدخلة [{(amountSum + _amount):N0}] أعلى من قيمة تكاليف المبادرة المعتمدة [{_initiative.ApprovedCosts:N0}]"
            };
        }
        
        _initiative.ContractStartsAfterInitiativeStart(_startDate, nameof(InitiativeContract.StartDate), Errors);
        _initiative.ContractEndsBeforeInitiativeFinish(_endDate, nameof(InitiativeContract.EndDate), Errors);
        
        return Errors;
    }
}