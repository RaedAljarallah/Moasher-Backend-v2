using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Persistence;

public class MoasherDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IMoasherDbContext
{
    public MoasherDbContext(DbContextOptions<MoasherDbContext> options) : base(options)
    {
        
    }

    public DbSet<Initiative> Initiatives => Set<Initiative>();
    public DbSet<Entity> Entities => Set<Entity>();
    public DbSet<EnumType> EnumTypes => Set<EnumType>();
    public DbSet<Portfolio> Portfolio => Set<Portfolio>();
    public DbSet<Program> Programs => Set<Program>();
    public DbSet<StrategicObjective> StrategicObjectives => Set<StrategicObjective>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        
        builder.ApplyConfigurationsFromAssembly(typeof(MoasherDbContext).Assembly);
    }
}