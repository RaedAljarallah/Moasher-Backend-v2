using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Validators;

public class InitiativeDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<Initiative> _initiatives;
    private readonly string _name;
    private readonly string _code;
    private readonly string? _codeByProgram;

    public InitiativeDomainValidator(List<Initiative> initiatives, string name, string code, string? codeByProgram)
    {
        _initiatives = initiatives;
        _name = name;
        _code = code;
        _codeByProgram = codeByProgram;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var initiative in _initiatives)
        {
            if (string.Equals(initiative.Name, _name, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(Initiative.Name)] = new[] {DomainValidationErrorMessages.Duplicated("اسم المبادرة")};
            }

            if (string.Equals(initiative.UnifiedCode, _code, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(Initiative.UnifiedCode)] = new[] {DomainValidationErrorMessages.Duplicated("رمز المبادرة الموحد")};
            }
            
            if (!string.IsNullOrWhiteSpace(_codeByProgram) 
                && string.Equals(initiative.CodeByProgram, _codeByProgram, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(Initiative.CodeByProgram)] = new[] {DomainValidationErrorMessages.Duplicated("رمز المبادرة في البرنامج")};
            }
        }

        return Errors;
    }
}