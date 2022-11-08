using System.Security.Claims;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Sub)?.ToGuid();
    
    public static string? GetEmail(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Email);

    public static string? GetName(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.name);

    public static bool HasPermission(this ClaimsPrincipal principal, Permission permission)
        => true;
    
    private static string? GetValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value; 
}