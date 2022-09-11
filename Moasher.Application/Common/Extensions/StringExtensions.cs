using System;

namespace Moasher.Application.Common.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string guid) => Guid.Parse(guid);

    public static string ToCamelCase(this string text)
    {
        return string.IsNullOrWhiteSpace(text) 
            ? text 
            : string.Concat(text[..1].ToLower(), text.AsSpan(1));
    }
}