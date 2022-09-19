using Moasher.Application.Features.Entities;
using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.KPIs.Commands;
using Moasher.Application.Features.StrategicObjectives;

namespace Moasher.Application.Features.KPIs.Queries.EditKPI;

public record EditKPIDto : KPICommandBase
{
    public Guid Id { get; set; }
    public EnumTypeDto? Status { get; set; }
    public EntityDto Entity { get; set; } = default!;
    public StrategicObjectiveDtoBase LevelThreeStrategicObjective { get; set; } = default!;
    public StrategicObjectiveDtoBase? LevelFourStrategicObjective { get; set; }
}