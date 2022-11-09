using System.ComponentModel.DataAnnotations;

namespace Moasher.Authentication.Pages.Account.Activation;

public class InputModel
{
    [Required(ErrorMessage = "يجب إدخال كلمة المرور المؤقتة")]
    public string TempPassword { get; set; } = default!;
    
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