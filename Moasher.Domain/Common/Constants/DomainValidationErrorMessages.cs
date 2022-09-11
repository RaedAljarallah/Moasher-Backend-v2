namespace Moasher.Domain.Common.Constants;

public struct DomainValidationErrorMessages
{
    internal static string Duplicated(string fieldName) => $"{fieldName} موجود مسبقاً";
}