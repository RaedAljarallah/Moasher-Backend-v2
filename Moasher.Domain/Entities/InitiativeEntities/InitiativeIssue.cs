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
    
    [Display(Name = "الوصف")]
    public string Description { get; set; } = default!;
    
    [JsonIgnore]
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
    
    [Display(Name = "النطاق")]
    public Guid? ScopeEnumId { get; set; }
    
    [JsonIgnore]
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
    
    [Display(Name = "الحالة")]
    public Guid? StatusEnumId { get; set; }

    [JsonIgnore]
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

    [Display(Name = "الأثر")]
    public Guid? ImpactEnumId { get; set; }
    
    [Display(Name = "وصف الأثر")]
    public string ImpactDescription { get; set; } = default!;
    
    [Display(Name = "مصدر المعوق")]
    public string Source { get; set; } = default!;
    
    [Display(Name = "سبب المعوق")]
    public string Reason { get; set; } = default!;
    
    [Display(Name = "الحل المقترح")]
    public string Resolution { get; set; } = default!;
    
    [Display(Name = "تاريخ الحل المتوقع")]
    public DateTimeOffset? EstimatedResolutionDate { get; set; }
    
    [Display(Name = "رفع بتاريخ")]
    public DateTimeOffset RaisedAt { get; set; }
    
    [Display(Name = "رفع بواسطة")]
    public string RaisedBy { get; set; } = default!;
    
    [Display(Name = "أغلق بتاريخ")]
    public DateTimeOffset? ClosedAt { get; set; }
}