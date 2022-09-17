namespace Moasher.Domain.Common.Abstracts;

public abstract class AuditableDbEntity : DbEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public string CreatedBy { get; set; } = "Raed";
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}