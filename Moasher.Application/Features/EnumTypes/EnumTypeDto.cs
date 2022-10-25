using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.EnumTypes;

public record EnumTypeDto : DtoBase
{
    public string Category { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Style { get; set; } = default!;
    public float? LimitFrom { get; set; }
    public float? LimitTo { get; set; }
    public bool IsDefault { get; set; }
}