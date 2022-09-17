using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Validators;

public class EntityDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<Entity> _entities;
    private readonly string _name;
    private readonly string _code;

    public EntityDomainValidator(List<Entity> entities, string name, string code)
    {
        _entities = entities;
        _name = name;
        _code = code;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var entity in _entities)
        {
            if (entity.Name == _name)
            {
                Errors["Name"] = new[] {DomainValidationErrorMessages.Duplicated("اسم الجهة")};
            }

            if (entity.Code == _code)
            {
                Errors["Code"] = new[] {DomainValidationErrorMessages.Duplicated("رمز الجهة")};
            }
        }

        return Errors;
    }
}