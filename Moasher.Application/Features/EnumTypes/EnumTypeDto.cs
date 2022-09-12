using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.EnumTypes;

public record EnumTypeDto : DtoBase
{
    public string Category { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Style { get; set; } = default!;
    public IDictionary<string, string>? Metadata { get; set; }
}