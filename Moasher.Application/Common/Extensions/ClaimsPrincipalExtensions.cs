using System.Security.Claims;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Sub)?.ToGuid();
    
    public static string? GetEmail(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Email);

    public static string? GetGivenName(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.GivenName);
    
    public static string? GetFamilyName(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.FamilyName);
    
    private static string? GetValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value; 
}