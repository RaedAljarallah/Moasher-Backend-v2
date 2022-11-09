using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;

namespace Moasher.Authentication.Core.IdentityServer.Configurations;

public class AppAuthorizeInteractionResponseGenerator : AuthorizeInteractionResponseGenerator
{
    public AppAuthorizeInteractionResponseGenerator(ISystemClock clock,
        ILogger<AppAuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) : base(
        clock, logger, consent, profile)
    {
    }

    public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse? consent = null)
    {
        if (request.PromptModes.Contains("activation"))
        {
            var userId = request.Raw.Get("userId");
            request.Raw.Remove("prompt");
            var response = new InteractionResponse
            {
                RedirectUrl = $"/Account/Activation?id={userId}"
            };
        
            return response;
        }

        if (request.PromptModes.Contains("reset-password"))
        {
            var token = request.Raw.Get("token");
            var userId = request.Raw.Get("userId");
            request.Raw.Remove("prompt");
            var response = new InteractionResponse
            {
                RedirectUrl = $"/Account/ResetPassword?token={token}&id={userId}"
            };
        
            return response;
        }
        
        return await base.ProcessInteractionAsync(request, consent);
    }
}