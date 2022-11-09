using System.Security.Claims;
using Moasher.Application.Common.Constants;

namespace Moasher.Application.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Sub)?.ToGuid();
    
    public static string? GetEmail(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Email);

    public static string? GetName(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Name);

    public static bool IsSuperAdmin(this ClaimsPrincipal principal) =>
        AppRoles.IsSuperAdminRole(principal.GetRole());
    public static bool IsAdmin(this ClaimsPrincipal principal) =>
        AppRoles.IsAdminRole(principal.GetRole());

    public static bool IsDataAssurance(this ClaimsPrincipal principal) =>
        AppRoles.IsDataAssurance(principal.GetRole());

    public static bool IsFinancialOperator(this ClaimsPrincipal principal) =>
        AppRoles.IsFinancialOperator(principal.GetRole());

    public static bool IsExecutionOperator(this ClaimsPrincipal principal) =>
        AppRoles.IsExecutionOperator(principal.GetRole());

    public static bool IsKPIsOperator(this ClaimsPrincipal principal) =>
        AppRoles.IsKPIsOperator(principal.GetRole());

    public static bool IsEntityUser(this ClaimsPrincipal principal) =>
        AppRoles.IsEntityUser(principal.GetRole());

    public static bool IsFullAccessViewer(this ClaimsPrincipal principal) =>
        AppRoles.IsFullAccessViewer(principal.GetRole());

    private static string GetRole(this ClaimsPrincipal principal) =>
        principal.GetValue(AppRegisteredClaimNames.Role) ?? string.Empty;

    private static string? GetValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value; 
}