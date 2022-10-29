using System.Collections.ObjectModel;

namespace Moasher.Application.Common.Constants;

public static class AppRoles
{
    public const string SuperAdmin = nameof(SuperAdmin);
    public const string Admin = nameof(Admin);
    public const string DataAssurance = nameof(DataAssurance);
    public const string FinancialOperator = nameof(FinancialOperator);
    public const string ExecutionOperator = nameof(ExecutionOperator);
    public const string KPIsOperator = nameof(KPIsOperator);
    public const string EntityUser = nameof(EntityUser);
    public const string FullAccessViewer = nameof(FullAccessViewer);

    public static IReadOnlyList<string> OrganizationRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        SuperAdmin,
        Admin,
        DataAssurance,
        FinancialOperator,
        ExecutionOperator,
        KPIsOperator
    });

    public static bool IsOrganizationRole(string roleName) => OrganizationRoles.Any(r => r == roleName);
}