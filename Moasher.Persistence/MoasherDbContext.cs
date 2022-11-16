using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Enums;
using Moasher.Domain.Events.EditRequests;
using Moasher.Domain.Types;
using Moasher.Persistence.Extensions;
using Newtonsoft.Json;

namespace Moasher.Persistence;

public class MoasherDbContext : MoasherDbContextBase, IMoasherDbContext
{
    private readonly IBackgroundQueue _backgroundQueue;
    private ICurrentUser CurrentUser { get; }
    public MoasherDbContext(DbContextOptions<MoasherDbContext> options, IBackgroundQueue backgroundQueue,
        ICurrentUser currentUser) : base(options)
    {
        _backgroundQueue = backgroundQueue;
        CurrentUser = currentUser;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<UserNotification>().HasQueryFilter(n => n.UserId == CurrentUser.GetId());
        builder.Entity<EditRequest>()
            .HasQueryFilter(e => CurrentUser.IsDataAssurance() || e.RequestedBy == CurrentUser.GetEmail());
        builder.Entity<EditRequestSnapshot>()
            .HasQueryFilter(e => CurrentUser.IsDataAssurance() || e.EditRequest.RequestedBy == CurrentUser.GetEmail());
        
        builder.Entity<Entity>()
            .HasQueryFilter(x => CurrentUser.CanViewAllResources() || x.Id == CurrentUser.GetEntityId());
        
        builder.Entity<Initiative>()
            .HasQueryFilter(x => CurrentUser.CanViewAllResources() || (x.EntityId == CurrentUser.GetEntityId() && x.Visible));
        
        builder.Entity<InitiativeMilestone>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));

        builder.Entity<ContractMilestone>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Milestone.Initiative.EntityId == CurrentUser.GetEntityId() && x.Milestone.Initiative.Visible));
        
        builder.Entity<InitiativeDeliverable>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeImpact>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeIssue>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeRisk>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeTeam>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeApprovedCost>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeBudget>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeContract>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeProject>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible));
        
        builder.Entity<InitiativeProjectBaseline>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Project.Initiative.EntityId == CurrentUser.GetEntityId() && x.Project.Initiative.Visible));
        
        builder.Entity<InitiativeProjectProgress>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Project.Initiative.EntityId == CurrentUser.GetEntityId() && x.Project.Initiative.Visible));

        builder.Entity<InitiativeExpenditure>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Project != null && x.Project.Initiative.EntityId == CurrentUser.GetEntityId() &&
                 x.Project.Initiative.Visible) ||
                (x.Contract != null && x.Contract.Initiative.EntityId == CurrentUser.GetEntityId() &&
                 x.Contract.Initiative.Visible));
        
        builder.Entity<InitiativeExpenditureBaseline>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Project != null && x.Project.Initiative.EntityId == CurrentUser.GetEntityId() &&
                 x.Project.Initiative.Visible) ||
                (x.Contract != null && x.Contract.Initiative.EntityId == CurrentUser.GetEntityId() &&
                 x.Contract.Initiative.Visible));
        
        builder.Entity<Analytic>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.Initiative != null && x.Initiative.EntityId == CurrentUser.GetEntityId() && x.Initiative.Visible) ||
                (x.KPI != null && x.KPI.EntityId == CurrentUser.GetEntityId() && x.KPI.Visible));
        
        builder.Entity<KPI>()
            .HasQueryFilter(x => CurrentUser.CanViewAllResources() || (x.EntityId == CurrentUser.GetEntityId() && x.Visible));
        
        builder.Entity<KPIValue>()
            .HasQueryFilter(x =>
                CurrentUser.CanViewAllResources() ||
                (x.KPI.EntityId == CurrentUser.GetEntityId() && x.KPI.Visible));
        
    }
    
    public IQueryable<T>? GetSet<T>(string tableName)
    {
        return (IQueryable<T>?) GetType().GetProperty(tableName)?.GetValue(this, null);
    }

    public void RemoveEntity(object entity)
    {
        Remove(entity);
    }

    public void UpdateEntity(object entity)
    {
        Update(entity);
    }

    public Task<int> SaveChangesAsyncFromDomainEvent(CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsyncFromInternalProcess(CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        PreventOperationIfHasEditRequest();
        HandelAuditableEntries();
        await HandelEditRequests(cancellationToken);
        var events = GetDomainEvents();
        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchEvents(events);
        return result;
    }

    private List<DomainEvent> GetDomainEvents()
    {
        var entities = ChangeTracker
            .Entries<DbEntity>()
            .Where(e => e.Entity.HasDomainEvents())
            .Select(e => e.Entity).ToList();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ForEach(e => e.ClearDomainEvents());

        return domainEvents;
    }

    private void HandelAuditableEntries()
    {
        foreach (var entry in ChangeTracker.Entries<AuditableDbEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = CurrentUser.GetEmail() ?? string.Empty;
                    entry.Entity.CreatedAt = LocalDateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = CurrentUser.GetEmail() ?? "System";
                    entry.Entity.LastModified = LocalDateTime.Now;
                    break;
            }
        }

        foreach (var projectProgress in ChangeTracker.Entries<InitiativeProjectProgress>())
        {
            switch (projectProgress.State)
            {
                case EntityState.Added:
                    projectProgress.Entity.PhaseStartedAt = projectProgress.Entity.CreatedAt;
                    projectProgress.Entity.PhaseStartedBy = projectProgress.Entity.CreatedBy;
                    break;
                case EntityState.Modified:
                    projectProgress.Entity.PhaseEndedAt = projectProgress.Entity.LastModified;
                    projectProgress.Entity.PhaseEndedBy = projectProgress.Entity.LastModifiedBy;
                    break;
            }
        }
    }

    private async Task HandelEditRequests(CancellationToken cancellationToken = new())
    {
        if (CurrentUser.IsSuperAdmin() || CurrentUser.IsAdmin()) return;

        var editRequest = new EditRequest();
        var events = new Dictionary<string, object>();
        foreach (var entry in ChangeTracker.Entries<ApprovableDbEntity>()
                     .Where(c => c.State != EntityState.Unchanged)
                     .Where(c => c.State != EntityState.Detached)
                     .ToList())
        {
            if (entry.Entity.HasDomainEvents())
            {
                editRequest.HasEvents = true;
                var entryEvents = entry.Entity.DomainEvents;
                foreach (var domainEvent in entryEvents)
                {
                    var eventName = domainEvent.GetType().Name;
                    var eventArgumentName = domainEvent.GetType()
                        .GetProperties()
                        .Select(p => p.Name)
                        .FirstOrDefault();

                    if (string.IsNullOrWhiteSpace(eventArgumentName)) continue;
                    var eventArgumentProperty = domainEvent.GetType().GetProperty(eventArgumentName);
                    var eventArgumentValue = eventArgumentProperty?.GetValue(domainEvent, null);
                    if (eventArgumentValue is not null)
                    {
                        events.Add($"{eventName}.{eventArgumentProperty!.PropertyType.Name}", eventArgumentValue);
                    }
                }
            }

            var snapshot = new EditRequestSnapshot
            {
                ModelName = entry.Entity.GetType().Name,
                TableName = entry.Metadata.GetTableName() ?? string.Empty
            };

            var snapshotValues = new Dictionary<string, object?>();
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                {
                    snapshot.ModelId = Guid.Parse(property.CurrentValue?.ToString() ?? Guid.Empty.ToString());
                }

                var propertyName = property.Metadata.Name;
                switch (entry.State)
                {
                    case EntityState.Added:
                        snapshot.Type = EditRequestType.Create;
                        snapshotValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Modified:
                        snapshot.Type = EditRequestType.Update;
                        snapshotValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        snapshot.Type = EditRequestType.Delete;
                        snapshotValues[propertyName] = property.OriginalValue;
                        break;
                }
            }

            if (entry.State == EntityState.Added)
            {
                entry.Entity.Approved = false;
            }

            if (entry.State == EntityState.Deleted)
            {
                // We need to keep the entity's parent last status,
                // so we set Entity.Approved to true to prevent domain events from changing the status
                // instead we set Entity.HasDeleteRequest to true, so we can capture the edite request
                // we need to keep the entity on the db, so we change the tracker to Modified
                entry.Entity.Approved = true;
                entry.Entity.HasDeleteRequest = true;
                entry.State = EntityState.Modified;
            }

            if (entry.State == EntityState.Modified)
            {
                // We need to keep the entity's parent last status,
                // so we set Entity.Approved to true to prevent domain events from changing the status
                // instead we set Entity.HasUpdateRequest to true, so we can capture the edite request
                // we need to keep the entity on the db, so we replace current values with the original values by reloading the entity
                await entry.ReloadAsync(cancellationToken);
                entry.Entity.Approved = true;
                entry.Entity.HasUpdateRequest = true;
            }

            if (snapshotValues.Any())
            {
                snapshot.OriginalValues = JsonConvert.SerializeObject(snapshotValues);
                editRequest.Snapshots.Add(snapshot);
            }
        }

        if (editRequest.Snapshots.Any())
        {
            editRequest.Events = JsonConvert.SerializeObject(events);
            editRequest.Status = EditRequestStatus.Pending;
            editRequest.RequestedAt = LocalDateTime.Now;
            editRequest.RequestedBy = CurrentUser.GetEmail() ?? string.Empty;
            editRequest.AddDomainEvent(new EditRequestCreatedEvent(editRequest));
            EditRequests.Add(editRequest);
        }
    }

    private async Task DispatchEvents(List<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _backgroundQueue.QueueTask(ct => Task.Factory.StartNew(() => domainEvent as INotification, ct));
        }
    }

    private void PreventOperationIfHasEditRequest()
    {
        if (ChangeTracker.Entries<ApprovableDbEntity>().ToList().Any(entry => entry.Entity.HasEditRequest()))
        {
            throw new ValidationException("لا يمكن تنفيذ العملية بسبب وجود طلب تعديل قائم");
        }
    }
}