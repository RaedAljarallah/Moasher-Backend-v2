namespace Moasher.Application.Common.Constants;

public struct ValidationErrorMessages
{
    internal static string NotEmpty(string fieldName) => $"يجب إدخال {fieldName}";
    internal static string EmailAddress() => "يجب إدخال البريد الإلكتروني بشكل صحيح";
    internal static string MaximumLength(string fieldName) => $"عدد خانات {fieldName} يجب أن لا تزيد عن" + " {MaxLength}";
    internal static string UserNotExists() => "البريد الإلكتروني أو كلمة المرور غير صحيحة";
    internal static string WrongFormat(string fieldName) => $"يجب إدخال {fieldName} بشكل صحيح";
    internal static string Duplicated(string fieldName) => $"{fieldName} موجود مسبقاً";
    internal static string WrongYearRange(string fieldName) => $"{fieldName} يجب أن تكون بين 1980-2080";
}