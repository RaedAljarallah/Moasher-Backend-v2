using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Entities;

public class Analytic : AuditableDbEntity
{
    private Initiative? _initiative;
    private KPI? _kpi;

    public string Description { get; set; } = default!;
    public DateTimeOffset AnalyzedAt { get; set; }
    public string AnalyzedBy { get; set; } = default!;
    public string Model { get; private set; } = default!;
    public string? InitiativeName { get; private set; }
    public Initiative? Initiative
    {
        get => _initiative;
        set
        {
            _initiative = value;
            InitiativeName = value?.Name;
        }
    }
    
    public string? KPIName { get; private set; }
    public KPI? KPI
    {
        get => _kpi;
        set
        {
            _kpi = value;
            KPIName = value?.Name;
        }
    }
    public Guid? KPIId { get; set; }
    
    
}