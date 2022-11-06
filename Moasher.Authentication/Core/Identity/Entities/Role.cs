using Microsoft.AspNetCore.Identity;

namespace Moasher.Authentication.Core.Identity.Entities;

public class Role : IdentityRole<Guid>
{
    public string LocalizedName { get; set; } = default!;
}