using Moasher.Domain.Entities;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Common.Utilities;

public static class InitiativeUtility
{
    public static EnumType? CalculateStatus(Progress progress, IEnumerable<EnumType> statusTypes)
    {
        return StatusCalculatorUtility.CalculateStatus(progress, statusTypes);
    }
}