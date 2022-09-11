using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Common.Abstracts;

public class AuditableDbEntity<TKey> : DbEntity<TKey> where TKey : IEquatable<TKey>
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.AddHours(3);
    // TODO: Replace dummy value
    public string CreatedBy { get; set; } = "Raed@Raed.com";
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}