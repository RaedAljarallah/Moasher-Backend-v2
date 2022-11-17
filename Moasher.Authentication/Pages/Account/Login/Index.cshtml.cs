using System.Text;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Moasher.Authentication.Core.Common.Attributes;
using Moasher.Authentication.Core.Common.Extensions;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.Identity.Extensions;

namespace Moasher.Authentication.Pages.Account.Login;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;
    private readonly IClientStore _clientStore;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    public ViewModel View { get; set; } = new();
    [BindProperty] public InputModel Input { get; set; } = new();

    public Index(IIdentityServerInteractionService interaction,
        IEventService events,
        IClientStore clientStore,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IPasswordHasher<User> passwordHasher)
    {
        _interaction = interaction;
        _events = events;
        _clientStore = clientStore;
        _userManager = userManager;
        _signInManager = signInManager;
        _passwordHasher = passwordHasher;
    }

    public async Task<IActionResult> OnGetAsync(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        View = new ViewModel {AllowRememberLogin = LoginOptions.AllowRememberLogin};
        Input.ReturnUrl = returnUrl;
        return await IsValidClient(context) is false
            ? NotFound()
            : Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        if (await IsValidClient(context) is false)
        {
            return NotFound();
        }

        View = new ViewModel {AllowRememberLogin = LoginOptions.AllowRememberLogin};
        
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
                return Page();
            }

            var isValidPassword = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Input.Password) ==
                                  PasswordVerificationResult.Success;
            if (isValidPassword)
            {
                if (user.Suspended)
                {
                    ModelState.AddModelError(string.Empty, LoginOptions.LockoutErrorMessage);
                    return Page();
                }

                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
                    return Page();
                }
                
                if (user.MustChangePassword)
                {
                    var token = Base64UrlEncoder.Encode(await _userManager.GeneratePasswordChangingToken(user));
                    var userId = Base64UrlEncoder.Encode(user.Id.ToString());
                    return RedirectToPage("../ChangePassword/Index", new {token, userId, Input.ReturnUrl});
                }
            }

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberLogin,
                lockoutOnFailure: true);
            if (result.Succeeded)
            {
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName, clientId: context?.Client.ClientId));

                if (context is not null)
                {
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage(Input.ReturnUrl);
                    }
                    
                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(Input.ReturnUrl);
                }
                
                // request for a local page
                if (Url.IsLocalUrl(Input.ReturnUrl))
                {
                    return Redirect(Input.ReturnUrl);
                }
                
                if (string.IsNullOrEmpty(Input.ReturnUrl))
                {
                    return Redirect("~/");
                }
                
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }
            
            await _events.RaiseAsync(new UserLoginFailureEvent(Input.Email, "invalid credentials", clientId:context?.Client.ClientId));
            ModelState.AddModelError(string.Empty,
                result.IsLockedOut ? LoginOptions.LockoutErrorMessage : LoginOptions.InvalidCredentialsErrorMessage);
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