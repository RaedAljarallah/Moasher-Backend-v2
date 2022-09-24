using Moasher.Application.Common.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.InitiativeTeams;

public record InitiativeTeamDto : DtoBase
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public EnumValue Role { get; set; } = default!;
    public string InitiativeName { get; set; } = default!;
    public Guid InitiativeId { get; set; }
}