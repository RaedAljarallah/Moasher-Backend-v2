using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Enums;

public enum Frequency : byte
{
    [Display(Name = "سنوي")]
    Annually = 1,

    [Display(Name = "نصف سنوي")]
    SemiAnnually = 2,

    [Display(Name = "ربع سنوي")]
    Quarterly = 3,

    [Display(Name = "شهري")]
    Monthly = 4
}