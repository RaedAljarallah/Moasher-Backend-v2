namespace Moasher.Application.Common.Abstracts;

public abstract record DtoBase
{
    public Guid Id { get; init; }
    public bool Approved { get; set; } = true;
    public AuditDto? Audit { get; set; }
}