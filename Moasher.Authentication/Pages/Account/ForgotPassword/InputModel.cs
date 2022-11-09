using System.ComponentModel.DataAnnotations;

namespace Moasher.Authentication.Pages.Account.ForgotPassword;

public class InputModel
{
    [Required(ErrorMessage = "يجب إدخال البريد الإلكتروني")]
    [EmailAddress(ErrorMessage = "يجب إدخال البريد الإلكتروني بشكل صحيح")]
    public string Email { get; set; } = default!;
    public string ReturnUrl { get; set; } = default!;
}