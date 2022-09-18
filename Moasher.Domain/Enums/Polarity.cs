using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Enums;

public enum Polarity : byte
{
    [Display(Name = "موجب")]
    Positive = 1,

    [Display(Name = "سالب")]
    Negative = 2
}