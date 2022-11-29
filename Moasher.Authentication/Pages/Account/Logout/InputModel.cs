namespace Moasher.Authentication.Pages.Account.Logout;

public class InputModel
{
    public string? LogoutId { get; set; }
    public string? Jti { get; set; }
    public long? Exp { get; set; }
}