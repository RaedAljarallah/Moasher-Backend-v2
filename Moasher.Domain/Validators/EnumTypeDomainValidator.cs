using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Validators;

public class EnumTypeDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<EnumType> _enumTypes;
    private readonly string _name;
    private readonly EnumTypeCategory _category;

    public EnumTypeDomainValidator(List<EnumType> enumTypes, string name, EnumTypeCategory category)
    {
        _enumTypes = enumTypes;
        _name = name;
        _category = category;
    }
    public IDictionary<string, string[]> Validate()
    {
        if (_enumTypes.Any(e => e.Name == _name && e.Category == _category.ToString()))
        {
            Errors["Name"] = new[] {DomainValidationErrorMessages.Duplicated("الاسم")};
        }

        return Errors;
    }
}