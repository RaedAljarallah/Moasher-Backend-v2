using Moasher.Domain.Enums;
using Moasher.Domain.Types;

namespace Moasher.Application.Common.Services;

public static class DateTimeService
{
    public static int GetCurrentQuarter()
    {
        return Math.Abs((LocalDateTime.Now.Month - 1) / 3) + 1;
    }

    public static int GetMonthQuarter(int month)
    {
        return Math.Abs((month - 1) / 3) + 1;
    }

    public static int[] GetCurrentQuarterMonths()
    {
        var firstMonthOfCurrentQuarter = (GetCurrentQuarter() - 1) * 3 + 1;
        return new[] { firstMonthOfCurrentQuarter, firstMonthOfCurrentQuarter + 1, firstMonthOfCurrentQuarter + 2 };
    }

    public static IEnumerable<YearsMonthsRange> GetYearsMonthsRange(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate.Year == endDate.Year)
        {
            yield return new YearsMonthsRange
            {
                Year = startDate.Year,
                Months = Enumerable.Range(startDate.Month, endDate.Month - startDate.Month + 1)
            };
        }

        foreach (var year in Enumerable.Range(startDate.Year, endDate.Year - startDate.Year + 1))
        {
            var startMonth = 1;
            var lastMonth = 12;
            if (year == startDate.Year)
            {
                startMonth = startDate.Month;
            }

            if (year == endDate.Year)
            {
                lastMonth = endDate.Month;
            }

            yield return new YearsMonthsRange
            {
                Year = year,
                Months = Enumerable.Range(startMonth, lastMonth - startMonth + 1)
            };
        }
    }
}

public record YearsMonthsRange
{
    public int Year { get; set; }
    public IEnumerable<int> Months { get; set; } = Enumerable.Empty<int>();
}