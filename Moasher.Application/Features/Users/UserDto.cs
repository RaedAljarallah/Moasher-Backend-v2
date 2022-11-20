using Moasher.Application.Common.Constants;

namespace Moasher.Application.Features.Users;

public record UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string LocalizedRole => AppRoles.GetLocalizedName(Role);
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public bool ReceiveEmailNotification { get; set; }
    public bool IsActive { get; set; }
    public bool IsSuspended { get; set; }
    public Guid EntityId { get; set; }
}