using Microsoft.EntityFrameworkCore;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Entities.StrategicObjectiveEntities;

public class StrategicObjective : AuditableDbEntity
{
    public HierarchyId HierarchyId { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public ICollection<Initiative> Initiatives { get; set; }
        = new HashSet<Initiative>();
    // public ICollection<KPI> KPIs { get; set; }
    //     = new HashSet<KPI>();
}