using Moasher.Domain.Common.Abstracts;

namespace Moasher.Domain.ValueObjects;

public class EnumValue : ValueObject
{
    public string? Name { get; private set; }
    public string? Style { get; private set; }

    public EnumValue() { }
    public EnumValue(string name, string style)
    {
        Name = name;
        Style = style;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name ?? string.Empty;
        yield return Style ?? string.Empty;
    }
}