using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Moasher.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "معوقات")]
public class InitiativeIssue : InitiativeRelatedDbEntity
{
    private EnumType _scopeEnum = default!;
    private EnumType _statusEnum = default!;
    private EnumType _impactEnum = default!;
    public string Description { get; set; } = default!;
    public EnumValue Scope { get; private set; } = default!;

    [JsonIgnore]
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
    public EnumValue Status { get; set; } = default!;

    [JsonIgnore]
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

    public EnumValue Impact { get; set; } = default!;

    [JsonIgnore]
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
    public string Source { get; set; } = default!;
    public string Reason { get; set; } = default!;
    public string Resolution { get; set; } = default!;
    public DateTimeOffset? EstimatedResolutionDate { get; set; }
    public DateTimeOffset RaisedAt { get; set; }
    public string RaisedBy { get; set; } = default!;
    public DateTimeOffset? ClosedAt { get; set; }
}