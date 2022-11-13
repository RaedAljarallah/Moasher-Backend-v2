using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities;

public class EnumType : AuditableDbEntity, IRootEntity
{
    public string Category { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Style { get; set; } = default!;
    public float? LimitFrom { get; set; }
    public float? LimitTo { get; set; }
    public bool IsDefault { get; set; }
    public bool CanBeDeleted { get; set; } = true;
}