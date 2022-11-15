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
using Z.EntityFramework.Plus;

namespace Moasher.Persistence;

public class MoasherDbContext : MoasherDbContextBase, IMoasherDbContext
{
    private readonly IBackgroundQueue _backgroundQueue;
    private readonly ICurrentUser _currentUser;
    public MoasherDbContext(DbContextOptions<MoasherDbContext> options, IBackgroundQueue backgroundQueue,
        ICurrentUser currentUser) : base(options)
    {
        _backgroundQueue = backgroundQueue;
        _currentUser = currentUser;
    }

    // private void ApplyGlobalQueryFilters()
    // {
    //     if (CurrentUser.IsEntityUser())
    //     {
    //         this.Filter<Initiative>(q => q.Where(i => i.EntityId == CurrentUser.GetEntityId()));
    //     }
    //     
    // }
    public ICurrentUser CurrentUser => _currentUser;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //ApplyGlobalQueryFilters();
        // builder.Entity<UserNotification>().HasQueryFilter(n => n.UserId == CurrentUser.GetId());
        // builder.Entity<EditRequest>().HasQueryFilter(e => !CurrentUser.IsDataAssurance() && e.RequestedBy == CurrentUser.GetEmail());


        // builder.Entity<Entity>()
        //     .HasQueryFilter(x => x.Id == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());

        // builder.Entity<Initiative>()
        //     .HasQueryFilter(x => x.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());


        // builder.Entity<InitiativeApprovedCost>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        //
        // builder.Entity<InitiativeBudget>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeContract>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeProject>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeProjectBaseline>()
        //     .HasQueryFilter(x => x.Project.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeProjectProgress>()
        //     .HasQueryFilter(x => x.Project.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeExpenditure>()
        //     .HasQueryFilter(x => x.Project != null && x.Project.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeExpenditure>()
        //     .HasQueryFilter(x => x.Contract != null && x.Contract.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeExpenditureBaseline>()
        //     .HasQueryFilter(x => x.Project != null && x.Project.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeExpenditureBaseline>()
        //     .HasQueryFilter(x => x.Contract != null && x.Contract.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeDeliverable>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeImpact>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeIssue>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeMilestone>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeRisk>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<InitiativeTeam>()
        //     .HasQueryFilter(x => x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<KPI>()
        //     .HasQueryFilter(x => x.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<KPIValue>()
        //     .HasQueryFilter(x => x.KPI.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<Analytic>()
        //     .HasQueryFilter(x => x.Initiative != null && x.Initiative.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
        // builder.Entity<Analytic>()
        //     .HasQueryFilter(x => x.KPI != null && x.KPI.EntityId == CurrentUser.GetEntityId() || CurrentUser.CanViewAllResources());
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