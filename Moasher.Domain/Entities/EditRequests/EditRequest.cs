using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities.EditRequests;

public class EditRequest : DbEntity
{
    public string Code { get; set; } = default!;
    public EditRequestStatus Status { get; set; }
    public DateTimeOffset RequestedAt { get; set; }
    public string RequestedBy { get; set; } = default!;
    public string? ActionBy { get; set; }
    public string? Justification { get; set; }
    public DateTimeOffset? ActionAt { get; set; }
    public bool HasEvents { get; set; }
    public string? Events { get; set; }
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