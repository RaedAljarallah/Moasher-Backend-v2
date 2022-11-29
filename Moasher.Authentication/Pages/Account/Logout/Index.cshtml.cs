using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moasher.Authentication.Core.Common.Attributes;
using Moasher.Authentication.Core.Common.Extensions;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.Identity.Services;

namespace Moasher.Authentication.Pages.Account.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;
    private readonly IInvalidToken _invalidToken;
    [BindProperty] public InputModel Input { get; set; } = new();

    public Index(SignInManager<User> signInManager, IIdentityServerInteractionService interaction, IEventService events,
        IInvalidToken invalidToken)
    {
        _signInManager = signInManager;
        _interaction = interaction;
        _events = events;
        _invalidToken = invalidToken;
    }

    public async Task<IActionResult> OnGetAsync(string logoutId)
    {
        Input.LogoutId = logoutId;
        var showLogoutPrompt = LogoutOptions.ShowLogoutPrompt;
        if (User.Identity?.IsAuthenticated != true)
        {
            // if the user is not authenticated, then just show logged out page
            showLogoutPrompt = false;
        }
        else
        {
            var context = await _interaction.GetLogoutContextAsync(Input.LogoutId);

            // capture the invalid token information 

            var tokenId = context.Parameters.Get("jti");
            var expiration = context.Parameters.Get("exp");
            if (tokenId is not null && expiration is not null)
            {
                if (long.TryParse(expiration, out var exp))
                {
                    Input.Jti = tokenId;
                    Input.Exp = exp;
                }
            }

            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                showLogoutPrompt = false;
            }
        }


        if (showLogoutPrompt == false)
        {
            // if the request for logout was properly authenticated from IdentityServer, then
            // we don't need to show the prompt and can just log the user out directly.
            return await OnPostAsync();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            if (Input.Jti is not null && Input.Exp.HasValue)
            {
                await _invalidToken.CreateAsync(Input.Jti, Input.Exp.Value);
            }
            
            // if there's no current logout context, we need to create one
            // this captures necessary info from the current logged in user
            // this can still return null if there is no context needed
            Input.LogoutId ??= await _interaction.CreateLogoutContextAsync();

            // delete local authentication cookie
            await _signInManager.SignOutAsync();

            // raise the logout event
            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));

            // see if we need to trigger federated logout
            var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

            // if it's a local login we can ignore this workflow
            if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
            {
                // we need to see if the provider supports external logout
                if (await HttpContext.CustomGetSchemeSupportsSignOutAsync(idp))
                {
                    // build a return URL so the upstream provider will redirect back
                    // to us after the user has logged out. this allows us to then
                    // complete our single sign-out processing.
                    var url = Url.Page("/Account/Logout/LoggedOut", new {logoutId = Input.LogoutId});

                    // this triggers a redirect to the external provider for sign-out
                    return SignOut(new AuthenticationProperties {RedirectUri = url}, idp);
                }
            }
        }

        return RedirectToPage("/Account/Logout/LoggedOut", new {logoutId = Input.LogoutId});
    }
}