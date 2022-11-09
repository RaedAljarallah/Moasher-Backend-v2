namespace Moasher.Domain.Enums;

public enum TimePeriod : byte
{
    // When you change any DisplayName you have to update ToListByFrequency in EnumExtensions
    MonthOne = 1,
    MonthTwo = 2,
    MonthThree = 3,
    MonthFour = 4,
    MonthFive = 5,
    MonthSix = 6,
    MonthSeven = 7,
    MonthEight = 8,
    MonthNine = 9,
    MonthTen = 10,
    MonthEleven = 11,
    MonthTwelve = 12,
    QuarterOne = 13,
    QuarterTwo = 14,
    QuarterThree = 15,
    QuarterFour = 16,
    FirstHalf = 17,
    SecondHalf = 18,
    EndOfYear = 19
}