using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities.EditRequests;

public class EditRequest : IDbEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public EditRequestType Type { get; set; }
    public EditRequestStatus Status { get; set; }
    public DateTimeOffset RequestedAt { get; set; }
    public string RequestedBy { get; set; } = default!;
    public string? ActionBy { get; set; }
    public DateTimeOffset? ActionAt { get; set; }
    public bool HasEvents { get; set; }
    public string? Events { get; set; }
    // public string? EventArguments { get; set; }
    // public string? EventArgumentTypes { get; set; }
    public ICollection<EditRequestSnapshot> Snapshots { get; set; }
        = new HashSet<EditRequestSnapshot>();

    public IEnumerable<string> GetEditScopes()
    {
        return Snapshots
            .Where(s => !s.ModelName.EndsWith("Baseline"))
            .Where(s => s.ModelName != nameof(InitiativeProjectProgress))
            .Select(s => s.ModelName)
            .Distinct()
            .ToList();
    }
}