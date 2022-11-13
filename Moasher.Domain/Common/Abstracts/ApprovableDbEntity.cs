namespace Moasher.Domain.Common.Abstracts;

public abstract class ApprovableDbEntity : AuditableDbEntity
{
    public bool Approved { get; set; }
    public bool HasDeleteRequest  { get; set; }
    public bool HasUpdateRequest { get; set; }
}