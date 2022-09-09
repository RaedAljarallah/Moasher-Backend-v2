using Microsoft.AspNetCore.Identity;

namespace Moasher.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Guid EntityId { get; set; }
    public string Role { get; set; } = default!;
    public bool MustChangePassword { get; set; } 
}