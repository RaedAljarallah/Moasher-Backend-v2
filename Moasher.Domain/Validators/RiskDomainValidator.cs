using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Validators;

public class RiskDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _description;

    public RiskDomainValidator(Initiative initiative, string description)
    {
        _initiative = initiative;
        _description = description;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var risk in _initiative.Risks)
        {
            if (string.Equals(risk.Description, _description, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(InitiativeRisk.Description)] = new[] {DomainValidationErrorMessages.Duplicated("وصف الخطر")};
            }
        }

        return Errors;
    }
}