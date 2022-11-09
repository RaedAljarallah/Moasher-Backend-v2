using System.Security.Claims;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;

namespace Moasher.Infrastructure.Authentication.Authorization;

public class CurrentUserService : ICurrentUser, ICurrentUserInitializer
{
    private ClaimsPrincipal? _user;

    public Guid? GetId() => IsAuthenticated() ? _user!.GetUserId() : Guid.Empty;
    public string? GetEmail() => IsAuthenticated() ? _user!.GetEmail() : string.Empty;
    public string? GetName() => IsAuthenticated() ? _user!.GetName() : string.Empty;
    public bool IsSuperAdmin() => IsAuthenticated() && _user!.IsSuperAdmin();
    public bool IsAdmin() => IsAuthenticated() && _user!.IsAdmin();
    public bool IsDataAssurance() => IsAuthenticated() && _user!.IsDataAssurance();
    public bool IsFinancialOperator() => IsAuthenticated() && _user!.IsFinancialOperator();
    public bool IsExecutionOperator() => IsAuthenticated() && _user!.IsExecutionOperator();
    public bool IsKPIsOperator() => IsAuthenticated() && _user!.IsKPIsOperator();
    public bool IsEntityUser() => IsAuthenticated() && _user!.IsEntityUser();
    public bool IsFullAccessViewer() => IsAuthenticated() && _user!.IsFullAccessViewer();
    public bool HasPermission(Permission permission) => IsAuthenticated() && GetPermissions().Contains(permission);

    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }

        _user = user;
    }

    private bool IsAuthenticated() => _user?.Identity?.IsAuthenticated is true;

    private IEnumerable<Permission> GetPermissions()
    {
        if (IsSuperAdmin()) return AppPermissions.SuperAdmin;
        if (IsAdmin()) return AppPermissions.Admin;
        if (IsDataAssurance()) return AppPermissions.DataAssurance;
        if (IsFinancialOperator()) return AppPermissions.FinancialOperator;
        if (IsExecutionOperator()) return AppPermissions.ExecutionOperator;
        if (IsKPIsOperator()) return AppPermissions.KPIsOperator;
        if (IsEntityUser()) return AppPermissions.EntityUser;
        if (IsFullAccessViewer()) return AppPermissions.FullAccessViewer;

        return Enumerable.Empty<Permission>();
    }
}