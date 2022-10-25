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
    private readonly bool _isDefault;

    public EnumTypeDomainValidator(List<EnumType> enumTypes, string name, EnumTypeCategory category, bool isDefault)
    {
        _enumTypes = enumTypes;
        _name = name;
        _category = category;
        _isDefault = isDefault;
    }

    public IDictionary<string, string[]> Validate()
    {
        foreach (var e in _enumTypes)
        {
            if (string.Equals(e.Name, _name, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(e.Category, _category.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(EnumType.Name)] = new[] {DomainValidationErrorMessages.Duplicated("الاسم")};
            }

            if (_isDefault && e.IsDefault is true and true)
            {
                Errors[nameof(EnumType.IsDefault)] = new[] {DomainValidationErrorMessages.Duplicated("القيمة الإفتراضية")};
            }
        }
        return Errors;
    }
}