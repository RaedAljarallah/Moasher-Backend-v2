using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Moasher.Application.Common.Types;

namespace Moasher.Infrastructure.Authentication.Authorization;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    private DefaultAuthorizationPolicyProvider DefaultPolicyProvider { get; }

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        DefaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }
    
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith($"{nameof(Permission)}", StringComparison.CurrentCultureIgnoreCase))
        {
            return DefaultPolicyProvider.GetPolicyAsync(policyName);
        }

        var policyBuilder = new AuthorizationPolicyBuilder();
        policyBuilder.AddRequirements(new PermissionRequirement(policyName));
        return Task.FromResult<AuthorizationPolicy?>(policyBuilder.Build());
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return DefaultPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return Task.FromResult<AuthorizationPolicy?>(null);
    }
}