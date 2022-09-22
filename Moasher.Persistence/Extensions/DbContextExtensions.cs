using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Services;
using Moasher.Domain.Common.Abstracts;

namespace Moasher.Persistence.Extensions;

public static class DbContextExtensions
{
    public static List<DomainEvent> GetDomainEvents(this DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<DbEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity).ToList();
        
        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();
        
        entities.ForEach(e => e.ClearDomainEvents());
        
        return domainEvents;
    }

    public static void UpdateAuditRecords(this DbContext context)
    {
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