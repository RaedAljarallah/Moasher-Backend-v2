using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Moasher.Authentication.Core.Common.Attributes;
using Moasher.Authentication.Core.Common.Constants;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.Mailing.Abstracts;
using Moasher.Authentication.Core.Mailing.Interfaces;

namespace Moasher.Authentication.Pages.Account.ForgotPassword;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly UserManager<User> _userManager;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IMailService _mailService;

    public Index(IIdentityServerInteractionService interaction, IClientStore clientStore, UserManager<User> userManager,
        IEmailTemplateService emailTemplateService, IMailService mailService)
    {
        _interaction = interaction;
        _clientStore = clientStore;
        _userManager = userManager;
        _emailTemplateService = emailTemplateService;
        _mailService = mailService;
    }

    [BindProperty] public InputModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        Input.ReturnUrl = returnUrl;
        return await IsValidClient(context) is false
            ? NotFound()
            : Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);
        if (await IsValidClient(context) is false)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user is null)
        {
            // Don't reveal that the user does not exist or is not confirmed
            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        if (!user.IsActive() || user.IsSuspended())
        {
            // Don't reveal that the user does not exist or is not confirmed
            return RedirectToPage("./ForgotPasswordConfirmation");
        }
        
        var token = Base64UrlEncoder.Encode(await _userManager.GeneratePasswordResetTokenAsync(user));
        var userId = Base64UrlEncoder.Encode(user.Id.ToString());
        var emailModel = new ResetPasswordEmailModel(user.GetFullName(), $"accounts/reset-password?token={token}&id={userId}");
        var emailTemplate = _emailTemplateService.GenerateEmailTemplate(EmailTemplates.ResetPassword, emailModel);
        var mailRequest = new MailRequest(new List<string> {user.Email}, "إعادة تعيين كلمة المرور", emailTemplate);
        _ = Task.Factory.StartNew(async () => await _mailService.SendAsync(mailRequest));

        return RedirectToPage("./ForgotPasswordConfirmation");
    }

    private async Task<bool> IsValidClient(AuthorizationRequest? context)
    {
        if (context?.Client.ClientId == null) return false;
        var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
        return client is {Enabled: true, EnableLocalLogin: true};
    }
}