namespace Moasher.Application.Common.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string guid) => Guid.Parse(guid);
}