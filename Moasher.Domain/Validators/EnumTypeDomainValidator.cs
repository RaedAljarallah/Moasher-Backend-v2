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
        if (_enumTypes.Any(e =>
                string.Equals(e.Name, _name, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(e.Category, _category.ToString(), StringComparison.CurrentCultureIgnoreCase)))
        {
            Errors[nameof(EnumType.Name)] = new[] {DomainValidationErrorMessages.Duplicated("الاسم")};
        }

        return Errors;
    }
}