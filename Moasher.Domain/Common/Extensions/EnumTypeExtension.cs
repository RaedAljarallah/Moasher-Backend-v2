using Moasher.Domain.Entities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Common.Extensions;

public static class EnumTypeExtension
{
    public static EnumValue ToEnumValue(this EnumType type)
    {
        return new EnumValue(type.Name, type.Style);
    }
}