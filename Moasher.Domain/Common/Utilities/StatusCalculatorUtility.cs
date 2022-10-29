using Moasher.Domain.Entities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Common.Utilities;

internal static class StatusCalculatorUtility
{
    public static EnumType? CalculateStatus(Progress progress, IEnumerable<EnumType> statusTypes)
    {
        EnumType? status = null;

        var types = statusTypes.ToList();
        if (!types.Any())
        {
            return status;
        }
        var diff = progress.Planned - progress.Actual;
        var orderedTypes = types.Where(t => !t.IsDefault).OrderBy(t => t.LimitFrom).ToList();

        if (diff < 1)
        {
            return orderedTypes.FirstOrDefault() ?? types.FirstOrDefault(e => e.IsDefault);
        }
        
        return orderedTypes.FirstOrDefault(t => diff >= t.LimitFrom && diff <= t.LimitTo) ??
               types.FirstOrDefault(e => e.IsDefault);
    }
}