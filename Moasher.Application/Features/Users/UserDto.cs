namespace Moasher.Application.Features.Users;

public record UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool IsLockedOut { get; set; }
    public Guid EntityId { get; set; }
}