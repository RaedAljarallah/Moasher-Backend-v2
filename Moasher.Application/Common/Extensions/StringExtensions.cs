
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
    public static string FirstCharToUpper(this string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.ToLower().AsSpan(1))
        };
    }
}