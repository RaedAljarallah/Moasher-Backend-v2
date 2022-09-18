using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities.KPIEntities;

public class KPI : AuditableDbEntity
{
    private EnumType? _statusEnum;
    private Entity _entity = default!;
    private StrategicObjective _levelOneStrategicObjective = default!;
    private StrategicObjective _levelTwoStrategicObjective = default!;
    private StrategicObjective _levelThreeStrategicObjective = default!;
    private StrategicObjective? _levelFourStrategicObjective;
    private Analytic? _latestAnalyticsModel;
    
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string OwnerName { get; set; } = default!;
    public string OwnerEmail { get; set; } = default!;
    public string OwnerPhoneNumber { get; set; } = default!;
    public string? OwnerPosition { get; set; }
    public Frequency Frequency { get; set; }
    public Polarity Polarity { get; set; }
    public ValidationStatus ValidationStatus { get; set; }
    public string? Formula { get; set; }
    public float? BaselineValue { get; set; }
    public short? BaselineYear { get; set; }
    public string MeasurementUnit { get; set; } = default!;
    public string? DataSource { get; set; }
    public string Description { get; set; } = default!;
    public float? PlannedProgress { get; set; }
    public float? ActualProgress { get; set; }
    public bool Visible { get; set; }
    public bool VisibleOnDashboard { get; set; }
    public bool CalculateStatus { get; set; }
    public EnumValue Status { get; private set; } = default!;

    public EnumType? StatusEnum
    {
        get => _statusEnum;
        set
        {
            _statusEnum = value;
            Status = value != null ? new EnumValue(value.Name, value.Style) : new EnumValue();
        }
    }

    public Guid? StatusEnumId { get; set; }
    public string EntityName { get; private set; } = default!;

    public Entity Entity
    {
        get => _entity;
        set
        {
            _entity = value;
            EntityName = value.Name;
        }
    }
    public Guid EntityId { get; set; }
    
    public string LevelOneStrategicObjectiveName { get; private set; } = default!;

    public StrategicObjective LevelOneStrategicObjective
    {
        get => _levelOneStrategicObjective;
        set
        {
            _levelOneStrategicObjective = value;
            LevelOneStrategicObjectiveName = value.Name;
            LevelOneStrategicObjectiveId = value.Id;
        }
    }

    public Guid LevelOneStrategicObjectiveId { get; private set; }

    public string LevelTwoStrategicObjectiveName { get; private set; } = default!;

    public StrategicObjective LevelTwoStrategicObjective
    {
        get => _levelTwoStrategicObjective;
        set
        {
            _levelTwoStrategicObjective = value;
            LevelTwoStrategicObjectiveName = value.Name;
            LevelTwoStrategicObjectiveId = value.Id;
        }
    }

    public Guid LevelTwoStrategicObjectiveId { get; private set; }

    public string LevelThreeStrategicObjectiveName { get; private set; } = default!;

    public StrategicObjective LevelThreeStrategicObjective
    {
        get => _levelThreeStrategicObjective;
        set
        {
            _levelThreeStrategicObjective = value;
            LevelThreeStrategicObjectiveName = value.Name;
        }
    }

    public Guid LevelThreeStrategicObjectiveId { get; set; }

    public string? LevelFourStrategicObjectiveName { get; private set; }

    public StrategicObjective? LevelFourStrategicObjective
    {
        get => _levelFourStrategicObjective;
        set
        {
            _levelFourStrategicObjective = value;
            LevelFourStrategicObjectiveName = value?.Name;
            LevelFourStrategicObjectiveId = value?.Id;
        }
    }

    public Guid? LevelFourStrategicObjectiveId { get; private set; }
    
    public string? LatestAnalytics { get; private set; }
    public DateTimeOffset? LatestAnalyticsDate { get; private set; }
    public Analytic? LatestAnalyticsModel
    {
        get => _latestAnalyticsModel;
        set
        {
            _latestAnalyticsModel = value;
            LatestAnalytics = value?.Description;
            LatestAnalyticsDate = value?.AnalyzedAt;
        }
    }
    public ICollection<Analytic> Analytics { get; set; }
        = new HashSet<Analytic>();

    public ICollection<KPIValue> Values { get; set; }
        = new HashSet<KPIValue>();
}