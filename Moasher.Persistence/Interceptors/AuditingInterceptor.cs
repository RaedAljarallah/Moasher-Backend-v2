using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Types;

namespace Moasher.Persistence.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
        {
            HandelAuditableEntries(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private static void HandelAuditableEntries(DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries<AuditableDbEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // TODO: Replace dummy value
                    entry.Entity.CreatedBy = "Test@Test.com";
                    entry.Entity.CreatedAt = LocalDateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "Test@Test.com";
                    entry.Entity.LastModified = LocalDateTime.Now;
                    break;
            }
            // TODO: Replace dummy value with edit request handler
            entry.Entity.Approved = true;
        }
    }
}