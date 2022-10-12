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
        statusTypes = types.OrderBy(e => float.Parse(e.Metadata?["diff"] ?? throw new InvalidOperationException())).ToList();
        var diff = progress.Planned - progress.Actual;

        return statusTypes.FirstOrDefault(type => diff <= float.Parse(type.Metadata?["diff"] ?? throw new InvalidOperationException()));
    }
}