using Microsoft.AspNetCore.Identity;
using Moasher.Domain.Common.Interfaces;

namespace Moasher.Domain.Entities;

public class Role : IdentityRole<Guid>, IDbEntity
{
    public Role() { }
    public Role(string roleName): base(roleName) { }

    public string LocalizedName { get; set; } = default!;
}