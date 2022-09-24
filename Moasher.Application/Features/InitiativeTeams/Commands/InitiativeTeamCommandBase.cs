namespace Moasher.Application.Features.InitiativeTeams.Commands;

public abstract record InitiativeTeamCommandBase
{
    private string _name = default!;
    private string _email = default!;
    private string _phone = default!;

    public string Name { get => _name; set => _name = value.Trim(); }
    public string Email { get => _email; set => _email = value.Trim(); }
    public string Phone { get => _phone; set => _phone = value.Trim(); }
    public Guid RoleEnumId { get; set; }
    public Guid InitiativeId { get; set; }
}