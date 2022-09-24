using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.InitiativeTeams.Commands;

namespace Moasher.Application.Features.InitiativeTeams.Queries.EditInitiativeTeam;

public record EditInitiativeTeamDto : InitiativeTeamCommandBase
{
    public Guid Id { get; set; }
    public EnumTypeDto Role { get; set; } = default!;
}