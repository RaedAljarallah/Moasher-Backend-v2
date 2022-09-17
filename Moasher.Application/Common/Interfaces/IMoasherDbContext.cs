using Microsoft.EntityFrameworkCore;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Application.Common.Interfaces;

public interface IMoasherDbContext
{
    public DbSet<Initiative> Initiatives { get; }
    public DbSet<Entity> Entities { get; }
    public DbSet<Program> Programs { get; }
    public DbSet<Portfolio> Portfolios { get; }
    public DbSet<StrategicObjective> StrategicObjectives { get; }
    public DbSet<EnumType> EnumTypes { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}