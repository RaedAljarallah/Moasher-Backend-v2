using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "مخاطر")]
public class InitiativeRisk : InitiativeRelatedDbEntity
{
    private EnumType _typeEnum = default!;
    private EnumType _priorityEnum = default!;
    private EnumType _probabilityEnum = default!;
    private EnumType _impactEnum = default!;
    private EnumType _scopeEnum = default!;

    public string Description { get; set; } = default!;
    
    public string TypeName { get; private set; } = default!;
    public string TypeStyle { get; private set; } = default!;
    
    [JsonIgnore]
    public EnumType TypeEnum
    {
        get => _typeEnum;
        set
        {
            _typeEnum = value;
            TypeName = value.Name;
            TypeStyle = value.Style;
        }
    }
    public Guid? TypeEnumId { get; set; }

    public string PriorityName { get; private set; } = default!;
    public string PriorityStyle { get; private set; } = default!;
    
    [JsonIgnore]
    public EnumType PriorityEnum
    {
        get => _priorityEnum;
        set
        {
            _priorityEnum = value;
            PriorityName = value.Name;
            PriorityStyle = value.Style;
        }
    }
    public Guid? PriorityEnumId { get; set; }

    public string ProbabilityName { get; private set; } = default!;
    public string ProbabilityStyle { get; private set; } = default!;

    [JsonIgnore]
    public EnumType ProbabilityEnum
    {
        get => _probabilityEnum;
        set
        {
            _probabilityEnum = value;
            ProbabilityName = value.Name;
            ProbabilityStyle = value.Style;
        }
    }
    public Guid? ProbabilityEnumId { get; set; }

    public string ImpactName { get; private set; } = default!;
    public string ImpactStyle { get; private set; } = default!;
    [JsonIgnore]
    public EnumType ImpactEnum
    {
        get => _impactEnum;
        set
        {
            _impactEnum = value;
            ImpactName = value.Name;
            ImpactStyle = value.Style;
        }
    }
    public Guid? ImpactEnumId { get; set; }
    public string ImpactDescription { get; set; } = default!;

    public string ScopeName { get; private set; } = default!;
    public string ScopeStyle { get; private set; } = default!;

    [JsonIgnore]
    public EnumType ScopeEnum
    {
        get => _scopeEnum;
        set
        {
            _scopeEnum = value;
            ScopeName = value.Name;
            ScopeStyle = value.Style;
        }
    }
    public Guid? ScopeEnumId { get; set; }
    
    public string? Owner { get; set; }
    public string ResponsePlane { get; set; } = default!;
    public DateTimeOffset RaisedAt { get; set; }
    public string RaisedBy { get; set; } = default!;
}