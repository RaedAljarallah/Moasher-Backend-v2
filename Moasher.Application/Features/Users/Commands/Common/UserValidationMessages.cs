namespace Moasher.Application.Features.Users.Commands.Common;

internal readonly struct UserValidationMessages
{
    internal static string WrongRole = "صلاحية المستخدم غير صحيحة";
    internal static string WrongRoleSelection = "لا يمكن لمستخدم الجهة المحددة الحصول على الصلاحية المحددة";
    internal static string WrongEntity = "الجهة المدخلة غير صحيحة";
    internal static string Duplicated = "المستخدم موجود مسبقاً";
}