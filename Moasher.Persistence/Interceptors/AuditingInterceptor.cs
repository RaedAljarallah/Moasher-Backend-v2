using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Types;

namespace Moasher.Persistence.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;

    public AuditingInterceptor(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
        {
            HandelAuditableEntries(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private void HandelAuditableEntries(DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries<AuditableDbEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUser.GetEmail() ?? string.Empty;
                    entry.Entity.CreatedAt = LocalDateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUser.GetEmail() ?? string.Empty;
                    entry.Entity.LastModified = LocalDateTime.Now;
                    break;
            }
        }
    }

    private void HandelApprovableEntries(DbContext context)
    {
        var approved = _currentUser.IsSuperAdmin() || _currentUser.IsAdmin();
        foreach (var entry in context.ChangeTracker.Entries<ApprovableDbEntity>())
        {
            entry.Entity.Approved = approved;
        }
    }
}