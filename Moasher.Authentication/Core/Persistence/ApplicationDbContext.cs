using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moasher.Authentication.Core.Identity.Entities;

namespace Moasher.Authentication.Core.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>().ToTable("Users", t => t.ExcludeFromMigrations());
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", t => t.ExcludeFromMigrations());
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", t => t.ExcludeFromMigrations());
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", t => t.ExcludeFromMigrations());
        builder.Entity<Role>().ToTable("Roles", t => t.ExcludeFromMigrations());
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", t => t.ExcludeFromMigrations());
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", t => t.ExcludeFromMigrations());
    }
}