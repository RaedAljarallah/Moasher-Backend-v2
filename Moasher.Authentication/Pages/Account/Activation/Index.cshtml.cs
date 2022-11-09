using System.Text;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moasher.Authentication.Core.Common.Attributes;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.Identity.Extensions;
using Moasher.Authentication.Pages.Account.Login;
using Microsoft.IdentityModel.Tokens;

namespace Moasher.Authentication.Pages.Account.Activation;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    [BindProperty] public InputModel Input { get; set; } = new();

    public Index(IIdentityServerInteractionService interaction, IClientStore clientStore, UserManager<User> userManager,
        IPasswordHasher<User> passwordHasher)
    {
        _interaction = interaction;
        _clientStore = clientStore;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public async Task<IActionResult> OnGetAsync(string id, string returnUrl)
    {
        id = Base64UrlEncoder.Decode(id);

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

        var token = Base64UrlEncoder.Encode(await _userManager.GenerateActivationToken(user));
        
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
            if (Input.Password == Input.TempPassword)
            {
                ModelState.AddModelError("Input.Password", "كلمة المرور يجب أن تكون مختلفة عن كلمة المرور المؤقتة");
                return Page();
            }
            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user is not null)
            {
                var token = Base64UrlEncoder.Decode(Input.Token);
                var isValidToken = await _userManager.VerifyActivationToken(user, token);
                if (isValidToken)
                {
                    var isValidPassword =
                        _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Input.TempPassword) ==
                        PasswordVerificationResult.Success;
                    if (isValidPassword)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, Input.TempPassword, Input.Password);
                        if (result.Succeeded)
                        {
                            user.EmailConfirmed = true;
                            user.MustChangePassword = false;
                            await _userManager.UpdateAsync(user);
                            return RedirectToPage("../Login/Index", new {returnUrl = Input.ReturnUrl});
                        }
                        
                        result.Errors.ToList().ForEach(error => ModelState.AddModelError(string.Empty, error.Description));
                        return Page();
                    }
                }
            }

            ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
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