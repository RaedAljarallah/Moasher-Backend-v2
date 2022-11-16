using Microsoft.EntityFrameworkCore;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Entities.StrategicObjectiveEntities;

namespace Moasher.Application.Common.Interfaces;

public interface IMoasherDbContext
{
    public DbSet<Initiative> Initiatives { get; }
    public DbSet<InitiativeApprovedCost> InitiativeApprovedCosts { get; }
    public DbSet<InitiativeBudget> InitiativeBudgets { get; }
    public DbSet<InitiativeContract> InitiativeContracts { get; }
    public DbSet<InitiativeProject> InitiativeProjects { get; }
    public DbSet<InitiativeProjectProgress> InitiativeProjectProgress { get; }
    public DbSet<InitiativeExpenditure> InitiativeExpenditures { get; }
    public DbSet<InitiativeExpenditureBaseline> InitiativeExpendituresBaseline { get; }
    public DbSet<InitiativeDeliverable> InitiativeDeliverables { get; }
    public DbSet<InitiativeImpact> InitiativeImpacts { get; }
    public DbSet<InitiativeIssue> InitiativeIssues { get; }
    public DbSet<InitiativeMilestone> InitiativeMilestones { get; }
    public DbSet<InitiativeRisk> InitiativeRisks { get; }
    public DbSet<InitiativeTeam> InitiativeTeams { get; }
    public DbSet<KPI> KPIs { get; }
    public DbSet<KPIValue> KPIValues { get; }
    public DbSet<Analytic> Analytics { get; }
    public DbSet<Entity> Entities { get; }
    public DbSet<Program> Programs { get; }
    public DbSet<Portfolio> Portfolios { get; }
    public DbSet<StrategicObjective> StrategicObjectives { get; }
    public DbSet<EnumType> EnumTypes { get; }
    public DbSet<User> Users { get; }
    public DbSet<Search> SearchRecords { get; }
    public DbSet<InvalidToken> InvalidTokens { get; }
    public DbSet<EditRequest> EditRequests { get; }
    public DbSet<UserNotification> UserNotifications { get; }
    public DbSet<ContractMilestone> ContractMilestones { get; }
    public IQueryable<T>? GetSet<T>(string tableName);
    public void RemoveEntity(object entity);
    public void UpdateEntity(object entity);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    public Task<int> SaveChangesAsyncFromDomainEvent(CancellationToken cancellationToken);
    public Task<int> SaveChangesAsyncFromInternalProcess(CancellationToken cancellationToken);
    
}