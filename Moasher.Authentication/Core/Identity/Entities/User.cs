using Microsoft.AspNetCore.Identity;

namespace Moasher.Authentication.Core.Identity.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Role { get; set; } = default!;
    public bool MustChangePassword { get; set; }
    public Guid EntityId { get; set; }
    public bool Suspended { get; set; }
    
    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}