using IdentityServer4.Validation;

namespace Moasher.Authentication.Core.IdentityServer.Configurations;

public class AppAuthorizeRequestValidator : ICustomAuthorizeRequestValidator
{
    public Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
    {
        var prompt = context.Result.ValidatedRequest.Raw.Get("prompt");
        if (!string.IsNullOrWhiteSpace(prompt) && 
            prompt.Equals("activation", StringComparison.CurrentCultureIgnoreCase))
        {
            context.Result.ValidatedRequest.PromptModes = new[] { "activation" };
        }
        
        if (!string.IsNullOrWhiteSpace(prompt) && 
            prompt.Equals("reset-password", StringComparison.CurrentCultureIgnoreCase))
        {
            context.Result.ValidatedRequest.PromptModes = new[] { "reset-password" };
        }
        
        return Task.CompletedTask;
    }
}