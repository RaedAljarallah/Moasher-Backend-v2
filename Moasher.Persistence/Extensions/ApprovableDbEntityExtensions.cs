using Moasher.Domain.Common.Abstracts;

namespace Moasher.Persistence.Extensions;

public static class ApprovableDbEntityExtensions
{
    public static bool HasEditRequest(this ApprovableDbEntity entity)
    {
        return !entity.Approved || entity.HasUpdateRequest || entity.HasDeleteRequest;
    }
}