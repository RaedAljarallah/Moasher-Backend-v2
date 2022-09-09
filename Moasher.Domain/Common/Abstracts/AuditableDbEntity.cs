using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Common.Abstracts;

public class AuditableDbEntity<TKey> : DbEntity<TKey> where TKey : IEquatable<TKey>
{
    [Display(Name = "انشأ بتاريخ")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.AddHours(3);
    [Display(Name = "انشأ بواسطة")]
    public string CreatedBy { get; set; } = default!;
    [Display(Name = "عدل بتاريخ")]
    public DateTimeOffset? LastModified { get; set; }
    [Display(Name = "عدل بواسطة")]
    public string? LastModifiedBy { get; set; }
}