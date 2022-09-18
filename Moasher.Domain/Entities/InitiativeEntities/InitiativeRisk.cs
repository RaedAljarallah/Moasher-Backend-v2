using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeRisk : InitiativeRelatedDbEntity
{
    private EnumType _typeEnum = default!;
    private EnumType _priorityEnum = default!;
    private EnumType _probabilityEnum = default!;
    private EnumType _impactEnum = default!;
    private EnumType _scopeEnum = default!;

    public string Description { get; set; } = default!;
    public EnumValue Type { get; set; } = default!;
    public EnumType TypeEnum
    {
        get => _typeEnum;
        set
        {
            _typeEnum = value;
            Type = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? TypeEnumId { get; set; }

    public EnumValue Priority { get; set; } = default!;
    public EnumType PriorityEnum
    {
        get => _priorityEnum;
        set
        {
            _priorityEnum = value;
            Priority = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? PriorityEnumId { get; set; }

    public EnumValue Probability { get; set; } = default!;

    public EnumType ProbabilityEnum
    {
        get => _probabilityEnum;
        set
        {
            _probabilityEnum = value;
            Probability = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? ProbabilityEnumId { get; set; }

    public EnumValue Impact { get; set; } = default!;

    public EnumType ImpactEnum
    {
        get => _impactEnum;
        set
        {
            _impactEnum = value;
            Impact = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? ImpactEnumId { get; set; }
    public string ImpactDescription { get; set; } = default!;

    public EnumValue Scope { get; set; } = default!;

    public EnumType ScopeEnum
    {
        get => _scopeEnum;
        set
        {
            _scopeEnum = value;
            Scope = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? ScopeEnumId { get; set; }
    
    public string? Owner { get; set; }
    public string ResponsePlane { get; set; } = default!;
    public DateTimeOffset RaisedAt { get; set; }
    public string RaisedBy { get; set; } = default!;
}