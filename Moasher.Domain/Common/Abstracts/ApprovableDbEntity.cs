namespace Moasher.Domain.Common.Abstracts;

public abstract class ApprovableDbEntity : AuditableDbEntity
{
    public bool Approved { get; set; }
}