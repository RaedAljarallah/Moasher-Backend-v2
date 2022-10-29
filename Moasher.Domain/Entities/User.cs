using Microsoft.AspNetCore.Identity;
using Moasher.Domain.Common.Interfaces;

namespace Moasher.Domain.Entities;

public class User : IdentityUser<Guid>, IDbEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Role { get; set; } = default!;
    public bool MustChangePassword { get; set; }
    public Entity Entity { get; set; } = default!;
    public Guid EntityId { get; set; }
}