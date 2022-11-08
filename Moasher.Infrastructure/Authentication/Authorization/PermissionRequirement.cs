using Microsoft.AspNetCore.Authorization;
using Moasher.Application.Common.Types;

namespace Moasher.Infrastructure.Authentication.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public Permission Permission { get; }

    public PermissionRequirement(Permission permission)
    {
        Permission = permission;
    }

    public PermissionRequirement(string policyName)
    {
        Permission = Permission.InitializeFromPolicy(policyName);
    }
}