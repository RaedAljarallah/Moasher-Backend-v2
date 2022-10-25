using Moasher.Domain.Enums;

namespace Moasher.Application.Features.EnumTypes.Commands;

public abstract record EnumTypeCommandBase
{
    private string _name = default!;
    private string _style = default!;
    
    public string Name { get => _name; set => _name = value.Trim(); }
    public string Style { get => _style; set => _style = value.Trim(); }
    public EnumTypeCategory Category { get; set; }
    public bool IsDefault { get; set; }
    public float? LimitFrom { get; set; }
    public float? LimitTo { get; set; }
}