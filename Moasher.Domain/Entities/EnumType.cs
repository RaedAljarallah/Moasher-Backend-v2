using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;

namespace Moasher.Domain.Entities;

public class EnumType : AuditableDbEntity
{
    public string Category { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Style { get; set; } = default!;
    public IDictionary<string, string>? Metadata { get; set; }
    public bool CanBeDeleted { get; set; } = true;
}