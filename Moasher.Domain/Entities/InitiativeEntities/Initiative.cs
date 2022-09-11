﻿using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class Initiative : AuditableDbEntity<Guid>
{
    private EnumType _statusEnum = default!;
    private EnumType _fundStatusEnum = default!;
    private Entity _entity = default!;
    private Portfolio? _portfolio;
    private Program _program = default!;
    private StrategicObjective _levelOneStrategicObjective = default!;
    private StrategicObjective _levelTwoStrategicObjective = default!;
    private StrategicObjective _levelThreeStrategicObjective = default!;
    private StrategicObjective? _levelFourStrategicObjective;

    public string UnifiedCode { get; set; } = default!;
    public string? CodeByProgram { get; set; }
    public string Name { get; set; } = default!;
    public string? Scope { get; set; }
    public string? TargetSegment { get; set; }
    public string? ContributionOnStrategicObjective { get; set; }
    public EnumValue Status { get; private set; } = default!;

    public EnumType StatusEnum
    {
        get => _statusEnum;
        set
        {
            _statusEnum = value;
            Status = new EnumValue(value.Name, value.Style);
        }
    }

    public Guid? StatusEnumId { get; set; }
    public EnumValue FundStatus { get; private set; } = default!;

    public EnumType FundStatusEnum
    {
        get => _fundStatusEnum;
        set
        {
            _fundStatusEnum = value;
            FundStatus = new EnumValue(value.Name, value.Style);
        }
    }

    public Guid FundStatusEnumId { get; set; }
    public DateTimeOffset PlannedStart { get; set; }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualStart { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public decimal RequiredCost { get; set; }
    public string? CapexCode { get; set; }
    public string? OpexCode { get; set; }
    public bool Visible { get; set; }
    public bool VisibleOnDashboard { get; set; }
    public bool CalculateStatus { get; set; }
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
    public string? PortfolioName { get; private set; }

    public Portfolio? Portfolio
    {
        get => _portfolio;
        set
        {
            _portfolio = value;
            PortfolioName = value?.Name;
        }
    }

    public Guid? PortfolioId { get; set; }
    public string ProgramName { get; private set; } = default!;

    public Program Program
    {
        get => _program;
        set
        {
            _program = value;
            ProgramName = value.Name;
        }
    }

    public Guid ProgramId { get; set; }
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
    public float? PlannedProgress { get; set; }
    public float? ActualProgress { get; set; }
}