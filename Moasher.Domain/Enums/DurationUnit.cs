using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Enums;

public enum DurationUnit : byte
{
    [Display(Name = "ساعة")]
    Hour = 1,

    [Display(Name = "يوم")]
    Day = 2,

    [Display(Name = "اسبوع")]
    Week = 3,

    [Display(Name = "شهر")]
    Month = 4,

    [Display(Name = "سنة")]
    Year = 5
}