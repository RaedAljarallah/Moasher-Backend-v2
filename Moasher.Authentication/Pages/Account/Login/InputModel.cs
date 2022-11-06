using System.ComponentModel.DataAnnotations;

namespace Moasher.Authentication.Pages.Account.Login;

public class InputModel
{
    [Required(ErrorMessage = "يجب إدخال البريد الإلكتروني")]
    [EmailAddress(ErrorMessage = "يجب إدخال البريد الإلكتروني بشكل صحيح")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "يجب إدخال كلمة المرور")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
        
    public bool RememberLogin { get; set; }
    public string ReturnUrl { get; set; } = default!;
}