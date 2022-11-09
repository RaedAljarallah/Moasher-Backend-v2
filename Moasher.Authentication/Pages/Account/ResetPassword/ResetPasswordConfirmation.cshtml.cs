using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moasher.Authentication.Core.Common.Attributes;

namespace Moasher.Authentication.Pages.Account.ResetPassword;

[SecurityHeaders]
[AllowAnonymous]
public class ResetPasswordConfirmation : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    public ResetPasswordConfirmationViewModel View { get; set; } = new();

    public ResetPasswordConfirmation(IIdentityServerInteractionService interaction, IClientStore clientStore)
    {
        _interaction = interaction;
        _clientStore = clientStore;
    }
    
    public async Task<IActionResult> OnGetAsync(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        View.RedirectUri = returnUrl;
        return await IsValidClient(context) is false
            ? NotFound()
            : Page();
    }
    
    private async Task<bool> IsValidClient(AuthorizationRequest? context)
    {
        if (context?.Client.ClientId == null) return false;
        var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
        return client is {Enabled: true, EnableLocalLogin: true};
    }
}