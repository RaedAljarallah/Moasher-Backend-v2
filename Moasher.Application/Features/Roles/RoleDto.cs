namespace Moasher.Application.Features.Roles;

public record RoleDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string LocalizedName { get; set; } = default!;
}