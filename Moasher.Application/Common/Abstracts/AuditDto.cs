using System.ComponentModel.DataAnnotations;

namespace Moasher.Application.Common.Abstracts;

public record AuditDto
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