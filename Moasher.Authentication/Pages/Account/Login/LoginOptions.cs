namespace Moasher.Authentication.Pages.Account.Login;

public class LoginOptions
{
    public static readonly bool AllowLocalLogin = true;
    public static readonly bool AllowRememberLogin = true;
    public static readonly TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    public static readonly string InvalidCredentialsErrorMessage = "خطأ في اسم المستخدم أو كلمة مرور.";
    public static readonly string InvalidUserInformationErrorMessage = "خطأ في معلومات الدخول.";
    public static readonly string LockoutErrorMessage = "تم إيقاف الحساب مؤقتاً، الرجاء المحاولة في وقت لاحق أو التواصل مع مدير النظام.";
}