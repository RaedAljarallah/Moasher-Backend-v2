using Moasher.Domain.Common.Abstracts;

namespace Moasher.Domain.ValueObjects;

public class EnumValue : ValueObject
{
    public string Name { get; }
    public string Style { get; }

    public EnumValue(string name, string style)
    {
        Name = name;
        Style = style;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Style;
    }
}