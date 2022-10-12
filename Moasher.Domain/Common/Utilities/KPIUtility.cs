using Moasher.Domain.Entities;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Common.Utilities;

public static class KPIUtility
{
    public static Progress CalculateProgress(float plannedValuesSum, float actualValuesSum, float? totalTargetValues, float variance, Polarity polarity)
    {
        var plannedProgress = plannedValuesSum > 0 && totalTargetValues > 0
            ? (float)(plannedValuesSum / totalTargetValues) * 100
            : 0f;

        var actualProgress = 0f;
        switch (actualValuesSum)
        {
            case > 0 when totalTargetValues > 0:
            {
                if (polarity == Polarity.Negative)
                {
                    actualValuesSum = plannedValuesSum - variance * -1;
                }

                actualProgress = (float)(actualValuesSum / totalTargetValues) * 100;
                break;
            }
        }

        return new Progress(plannedProgress, actualProgress);
    }

    public static EnumType? CalculateStatus(Progress progress, IEnumerable<EnumType> statusTypes)
    {
        return StatusCalculatorUtility.CalculateStatus(progress, statusTypes);
    }
}