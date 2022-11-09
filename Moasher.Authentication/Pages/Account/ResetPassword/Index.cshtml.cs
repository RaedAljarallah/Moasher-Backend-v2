using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Moasher.Authentication.Core.Common.Attributes;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.Identity.Extensions;

namespace Moasher.Authentication.Pages.Account.ResetPassword;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly UserManager<User> _userManager;

    [BindProperty] public InputModel Input { get; set; } = new();
    
    public Index(IIdentityServerInteractionService interaction, IClientStore clientStore, UserManager<User> userManager)
    {
        _interaction = interaction;
        _clientStore = clientStore;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(string token, string id, string returnUrl)
    {
        id = Base64UrlEncoder.Decode(id);
        token = Base64UrlEncoder.Decode(token);
        
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (await IsValidClient(context) is false)
        {
            return NotFound();
        }
        
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        var isValidToke = await _userManager.VerifyPasswordResetTokenAsync(user, token);
        if (!isValidToke)
        {
            return NotFound();
        }

        Input.ReturnUrl = returnUrl;
        Input.Token = token;
        Input.Id = id;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);
        if (await IsValidClient(context) is false)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user is not null && user.Id.ToString() == Input.Id)
            {
                var isValidToken = await _userManager.VerifyPasswordResetTokenAsync(user, Input.Token);
                if (isValidToken)
                {
                    var result = await _userManager.ResetPasswordAsync(user, Input.Token, Input.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToPage("./ResetPasswordConfirmation", new {returnUrl = Input.ReturnUrl});
                    }
                    
                    result.Errors.ToList().ForEach(error => ModelState.AddModelError(string.Empty, error.Description));
                    return Page();
                }
            }
            
            ModelState.AddModelError("Input.Email", "البريد الإلكتروني غير صحيح");
        }

        return Page();
    }
    private async Task<bool> IsValidClient(AuthorizationRequest? context)
    {
        if (context?.Client.ClientId == null) return false;
        var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
        return client is {Enabled: true, EnableLocalLogin: true};
    }
}