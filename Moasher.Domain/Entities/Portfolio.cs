using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Entities;

public class Portfolio : AuditableDbEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public ICollection<Initiative> Initiatives { get; set; }
        = new HashSet<Initiative>();
}