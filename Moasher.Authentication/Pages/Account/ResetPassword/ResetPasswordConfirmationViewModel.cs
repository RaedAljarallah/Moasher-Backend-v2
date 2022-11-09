namespace Moasher.Authentication.Pages.Account.ResetPassword;

public class ResetPasswordConfirmationViewModel
{
    public string? RedirectUri { get; set; }
    public bool AutomaticRedirectAfterResetPassword { get; set; } = true;
}