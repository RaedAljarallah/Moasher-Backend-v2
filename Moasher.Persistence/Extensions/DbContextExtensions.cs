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
    
}