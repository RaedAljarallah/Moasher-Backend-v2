using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moasher.Authentication.Core.Common.Attributes;

namespace Moasher.Authentication.Pages.Account.ForgotPassword;

[SecurityHeaders]
[AllowAnonymous]
public class ForgotPasswordConfirmation : PageModel
{
    public void OnGet()
    {
        
    }
}