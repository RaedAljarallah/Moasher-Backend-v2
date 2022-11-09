using System.ComponentModel.DataAnnotations;

namespace Moasher.Authentication.Pages.Account.ResetPassword;

public class InputModel
{
    [Required(ErrorMessage = "يجب إدخال البريد الإلكتروني")]
    [EmailAddress(ErrorMessage = "يجب إدخال البريد الإلكتروني بشكل صحيح")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "يجب إدخال كلمة المرور")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "كلمات المرور غير متطابقة")]
    public string ConfirmPassword { get; set; } = default!;
    
    public string ReturnUrl { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string Id { get; set; } = default!;
}