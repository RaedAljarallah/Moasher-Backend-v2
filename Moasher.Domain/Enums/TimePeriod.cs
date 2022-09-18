using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Enums;

public enum TimePeriod : byte
{
    // When you change any DisplayName you have to update ToListByFrequency in EnumExtensions
    [Display(Name = "شهر 1")]
    MonthOne = 1,

    [Display(Name = "شهر 2")]
    MonthTwo = 2,

    [Display(Name = "شهر 3")]
    MonthThree = 3,

    [Display(Name = "شهر 4")]
    MonthFour = 4,

    [Display(Name = "شهر 5")]
    MonthFive = 5,

    [Display(Name = "شهر 6")]
    MonthSix = 6,

    [Display(Name = "شهر 7")]
    MonthSeven = 7,

    [Display(Name = "شهر 8")]
    MonthEight = 8,

    [Display(Name = "شهر 9")]
    MonthNine = 9,

    [Display(Name = "شهر 10")]
    MonthTen = 10,

    [Display(Name = "شهر 11")]
    MonthEleven = 11,

    [Display(Name = "شهر 12")]
    MonthTwelve = 12,

    [Display(Name = "الربع الأول")]
    QuarterOne = 13,

    [Display(Name = "الربع الثاني")]
    QuarterTwo = 14,

    [Display(Name = "الربع الثالث")]
    QuarterThree = 15,

    [Display(Name = "الربع الرابع")]
    QuarterFour = 16,

    [Display(Name = "النصف الأول")]
    FirstHalf = 17,

    [Display(Name = "النصف الثاني")]
    SecondHalf = 18,

    [Display(Name = "نهاية السنة")]
    EndOfYear = 19
}