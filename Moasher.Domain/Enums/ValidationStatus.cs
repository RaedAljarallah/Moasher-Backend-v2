using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Enums;

public enum ValidationStatus : byte
{
    [Display(Name = "موثق")]
    Verified = 1,

    [Display(Name = "غير موثق")]
    Unverified = 2,

    [Display(Name = "لم يتم قياسة")]
    Unmeasured = 3
}