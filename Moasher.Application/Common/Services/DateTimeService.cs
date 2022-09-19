namespace Moasher.Application.Common.Services;

public static class DateTimeService
{
    public static DateTimeOffset Now => DateTimeOffset.UtcNow.AddHours(3);

    public static int GetCurrentQuarter()
    {
        return Math.Abs((Now.Month - 1) / 3) + 1;
    }

    public static int GetMonthQuarter(int month)
    {
        return Math.Abs((month - 1) / 3) + 1;
    }

    public static int[] GetCurrentQuarterMonths()
    {
        var firstMonthOfCurrentQuarter = ((GetCurrentQuarter() - 1) * 3) + 1;
        return new int[3] { firstMonthOfCurrentQuarter, firstMonthOfCurrentQuarter + 1, firstMonthOfCurrentQuarter + 2 };
    }
}