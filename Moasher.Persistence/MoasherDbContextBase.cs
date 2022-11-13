using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Entities.StrategicObjectiveEntities;
using Moasher.Persistence.Extensions;

namespace Moasher.Persistence;

public abstract class MoasherDbContextBase : IdentityDbContext<User, Role, Guid>
{
    protected MoasherDbContextBase(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Initiative> Initiatives => Set<Initiative>();
    public DbSet<InitiativeApprovedCost> InitiativeApprovedCosts => Set<InitiativeApprovedCost>();
    public DbSet<InitiativeBudget> InitiativeBudgets => Set<InitiativeBudget>();
    public DbSet<InitiativeContract> InitiativeContracts => Set<InitiativeContract>();
    public DbSet<InitiativeProject> InitiativeProjects => Set<InitiativeProject>();
    public DbSet<InitiativeProjectBaseline> InitiativeProjectsBaseline => Set<InitiativeProjectBaseline>();
    public DbSet<InitiativeProjectProgress> InitiativeProjectProgress => Set<InitiativeProjectProgress>();
    public DbSet<InitiativeExpenditure> InitiativeExpenditures => Set<InitiativeExpenditure>();
    public DbSet<InitiativeExpenditureBaseline> InitiativeExpendituresBaseline => Set<InitiativeExpenditureBaseline>();
    public DbSet<InitiativeDeliverable> InitiativeDeliverables => Set<InitiativeDeliverable>();
    public DbSet<InitiativeImpact> InitiativeImpacts => Set<InitiativeImpact>();
    public DbSet<InitiativeIssue> InitiativeIssues => Set<InitiativeIssue>();
    public DbSet<InitiativeMilestone> InitiativeMilestones => Set<InitiativeMilestone>();
    public DbSet<InitiativeRisk> InitiativeRisks => Set<InitiativeRisk>();
    public DbSet<InitiativeTeam> InitiativeTeams => Set<InitiativeTeam>();
    public DbSet<Entity> Entities => Set<Entity>();
    public DbSet<EnumType> EnumTypes => Set<EnumType>();
    public DbSet<Portfolio> Portfolios => Set<Portfolio>();
    public DbSet<Program> Programs => Set<Program>();
    public DbSet<StrategicObjective> StrategicObjectives => Set<StrategicObjective>();
    public DbSet<KPI> KPIs => Set<KPI>();
    public DbSet<KPIValue> KPIValues => Set<KPIValue>();
    public DbSet<Analytic> Analytics => Set<Analytic>();
    public DbSet<Search> SearchRecords => Set<Search>();
    public DbSet<InvalidToken> InvalidTokens => Set<InvalidToken>();
    public DbSet<EditRequest> EditRequests => Set<EditRequest>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");

        builder.ApplyConfigurationsFromAssembly(typeof(MoasherDbContext).Assembly);

        builder.SeedRoles();
        builder.SeedOrganizerEntity();
    }
}