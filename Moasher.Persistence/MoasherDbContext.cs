using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Enums;
using Moasher.Domain.Types;
using Newtonsoft.Json;

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

    public Task<int> SaveChangesAsyncFromDomainEvent(CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsyncFromInternalProcess(CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(cancellationToken);
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
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
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
                    entry.Entity.CreatedBy = _currentUser.GetEmail() ?? string.Empty;
                    entry.Entity.CreatedAt = LocalDateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUser.GetEmail() ?? "System";
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
        var approved = _currentUser.IsSuperAdmin() || _currentUser.IsAdmin();
        var editRequest = new EditRequest {Code = Guid.NewGuid().ToString()};
        var events = new Dictionary<string, object>();
        var hasEditRequest = false;

        foreach (var entry in ChangeTracker.Entries<ApprovableDbEntity>())
        {
            if (approved)
            {
                entry.Entity.Approved = true;
                continue;
            }

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
                        editRequest.Type = EditRequestType.Create;
                        snapshotValues[propertyName] = property.CurrentValue;
                        hasEditRequest = true;
                        break;
                    case EntityState.Modified:
                        editRequest.Type = EditRequestType.Update;
                        snapshotValues[propertyName] = property.CurrentValue;
                        hasEditRequest = true;
                        break;
                    case EntityState.Deleted:
                        editRequest.Type = EditRequestType.Delete;
                        snapshotValues[propertyName] = property.OriginalValue;
                        hasEditRequest = true;
                        break;
                }
            }

            if (editRequest.Type == EditRequestType.Delete)
            {
                // We need to keep the entity's parent last status,
                // so we set Entity.Approved to true to prevent domain events from changing the status
                // instead we set Entity.HasDeleteRequest to true, so we can capture the edite request
                // we need to keep the entity on the db, so we change the tracker to Modified
                entry.Entity.Approved = true;
                entry.Entity.HasDeleteRequest = true;
                entry.State = EntityState.Modified;
            }

            if (editRequest.Type == EditRequestType.Update)
            {
                // We need to keep the entity's parent last status,
                // so we set Entity.Approved to true to prevent domain events from changing the status
                // instead we set Entity.HasUpdateRequest to true, so we can capture the edite request
                // we need to keep the entity on the db, so we replace current values with the original values by reloading the entity
                //var ggg = entry.Entity;
                await entry.ReloadAsync(cancellationToken);
                //await entry.Reference("ScopeEnum").LoadAsync(cancellationToken);
                entry.Entity.Approved = true;
                entry.Entity.HasUpdateRequest = true;
            }

            if (snapshotValues.Any())
            {
                snapshot.OriginalValues = JsonConvert.SerializeObject(snapshotValues);
                editRequest.Snapshots.Add(snapshot);
            }
        }

        if (hasEditRequest)
        {
            editRequest.Events = JsonConvert.SerializeObject(events);
            editRequest.Status = EditRequestStatus.Pending;
            editRequest.RequestedAt = LocalDateTime.Now;
            editRequest.RequestedBy = _currentUser.GetEmail() ?? string.Empty;
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
}