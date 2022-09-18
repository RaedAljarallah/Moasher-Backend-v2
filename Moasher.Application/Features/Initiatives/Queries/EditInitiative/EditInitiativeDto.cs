using Moasher.Application.Features.Entities;
using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.Initiatives.Commands;
using Moasher.Application.Features.Programs;
using Moasher.Application.Features.StrategicObjectives;

namespace Moasher.Application.Features.Initiatives.Queries.EditInitiative;

public record EditInitiativeDto : InitiativeCommandBase
{
    public Guid Id { get; set; }
    public EnumTypeDto FundStatus { get; set; } = default!;
    public EnumTypeDto? Status { get; set; }
    public EntityDto Entity { get; set; } = default!;
    public ProgramDto Program { get; set; } = default!;
    public StrategicObjectiveDtoBase LevelThreeStrategicObjective { get; set; } = default!;
    public StrategicObjectiveDtoBase? LevelFourStrategicObjective { get; set; }
    // public PortfolioDto Portfolio { get; set; }
}