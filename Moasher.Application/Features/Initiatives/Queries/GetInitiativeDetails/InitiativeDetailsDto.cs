using Moasher.Application.Features.InitiativeTeams;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativeDetails;

public record InitiativeDetailsDto : InitiativeDto
{
    public IEnumerable<InitiativeTeamDto> Teams { get; set; } = Enumerable.Empty<InitiativeTeamDto>();
}