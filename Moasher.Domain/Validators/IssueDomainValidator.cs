using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Validators;

public class IssueDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _description;

    public IssueDomainValidator(Initiative initiative, string description)
    {
        _initiative = initiative;
        _description = description;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var issue in _initiative.Issues)
        {
            if (string.Equals(issue.Description, _description, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(InitiativeIssue.Description)] = new[] {DomainValidationErrorMessages.Duplicated("وصف المعوق")};
            }
        }

        return Errors;
    }
}