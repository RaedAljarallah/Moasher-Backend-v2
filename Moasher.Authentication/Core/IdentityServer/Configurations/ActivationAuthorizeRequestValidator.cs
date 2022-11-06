using IdentityServer4.Validation;

namespace Moasher.Authentication.Core.IdentityServer.Configurations;

public class ActivationAuthorizeRequestValidator : ICustomAuthorizeRequestValidator
{
    public Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
    {
        var prompt = context.Result.ValidatedRequest.Raw.Get("prompt");
        if (!string.IsNullOrWhiteSpace(prompt) && 
            prompt.Equals("activation", StringComparison.OrdinalIgnoreCase))
        {
            context.Result.ValidatedRequest.PromptModes = new[] { "activation" };
        }
        
        return Task.CompletedTask;
    }
}