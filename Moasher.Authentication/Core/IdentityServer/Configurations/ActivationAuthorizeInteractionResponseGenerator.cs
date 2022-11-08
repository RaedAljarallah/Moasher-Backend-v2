using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;

namespace Moasher.Authentication.Core.IdentityServer.Configurations;

public class ActivationAuthorizeInteractionResponseGenerator : AuthorizeInteractionResponseGenerator
{
    public ActivationAuthorizeInteractionResponseGenerator(ISystemClock clock,
        ILogger<ActivationAuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) : base(
        clock, logger, consent, profile)
    {
    }

    public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse? consent = null)
    {
        if (!request.PromptModes.Contains("activation"))
        {
            return await base.ProcessInteractionAsync(request, consent);
        }
        
        var userId = request.Raw.Get("userId");
        request.Raw.Remove("prompt");
        var response = new InteractionResponse
        {
            RedirectUrl = $"/account/activation?id={userId}",
            
        };
        
        return response;
    }
}