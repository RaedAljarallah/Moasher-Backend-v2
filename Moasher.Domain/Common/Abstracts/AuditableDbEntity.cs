namespace Moasher.Domain.Common.Abstracts;

public abstract class AuditableDbEntity : DbEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool Approved { get; set; }
}