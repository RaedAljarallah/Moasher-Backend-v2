using Moasher.Application.Common.Types;

namespace Moasher.Application.Common.Interfaces;

public interface ICurrentUser
{
    public Guid? GetId();
    public string? GetEmail();
    public string? GetName();
    public bool IsSuperAdmin();
    public bool IsAdmin();
    public bool IsDataAssurance();
    public bool IsFinancialOperator();
    public bool IsExecutionOperator();
    public bool IsKPIsOperator();
    public bool IsEntityUser();
    public bool IsFullAccessViewer();
    public bool HasPermission(Permission permission);
}