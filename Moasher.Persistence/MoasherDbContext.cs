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
}