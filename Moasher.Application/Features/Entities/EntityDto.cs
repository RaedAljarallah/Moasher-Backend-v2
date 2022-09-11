using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Entities;

public record EntityDto : DtoBase
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int InitiativesCount { get; set; }
    public int KPIsCount { get; set; }
    public int StrategicObjectivesCount { get; set; }
}