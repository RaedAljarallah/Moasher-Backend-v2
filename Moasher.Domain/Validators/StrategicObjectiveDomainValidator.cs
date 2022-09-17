using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Domain.Validators;

public class StrategicObjectiveDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<StrategicObjective> _strategicObjectives;
    private readonly string _name;
    private readonly string _code;

    public StrategicObjectiveDomainValidator(List<StrategicObjective> strategicObjectives, string name, string code)
    {
        _strategicObjectives = strategicObjectives;
        _name = name;
        _code = code;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var strategicObjective in _strategicObjectives)
        {
            if (strategicObjective.Name == _name)
            {
                Errors["Name"] = new[] {DomainValidationErrorMessages.Duplicated("اسم الهدف الإستراتيجي")};
            }

            if (strategicObjective.Code == _code)
            {
                Errors["Code"] = new[] {DomainValidationErrorMessages.Duplicated("رمز الهدف الإستراتيجي")};
            }
        }

        return Errors;
    }
}