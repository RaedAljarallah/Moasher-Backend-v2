using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Abstracts;

namespace Moasher.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private static void UpdateEntities(DbContext? context)
    {
        if (context is null) return;
        foreach (var entry in context.ChangeTracker.Entries<AuditableDbEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // TODO: Replace dummy value
                    entry.Entity.CreatedBy = "Test@Test.com";
                    entry.Entity.CreatedAt = DateTimeService.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "Test@Test.com";
                    entry.Entity.LastModified = DateTimeService.Now;
                    break;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}