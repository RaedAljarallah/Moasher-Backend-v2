using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.StrategicObjectives;

public record StrategicObjectiveDtoBase : DtoBase
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string HierarchyId { get; set; } = default!;
    public short Level { get; set; }
    public int InitiativesCount { get; set; }
    public int KPIsCount { get; set; }
}