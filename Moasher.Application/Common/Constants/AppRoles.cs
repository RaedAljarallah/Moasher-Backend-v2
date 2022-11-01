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

    public static IReadOnlyList<string> AllRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        SuperAdmin,
        Admin,
        DataAssurance,
        FinancialOperator,
        ExecutionOperator,
        KPIsOperator,
        EntityUser,
        FullAccessViewer
    });

    public static bool IsOrganizationRole(string roleName) => OrganizationRoles.Any(r => r == roleName);

    public static string GetSuperAdminRole() => SuperAdmin;

    public static bool IsSuperAdminRole(string role) =>
        string.Equals(SuperAdmin, role, StringComparison.CurrentCultureIgnoreCase);

    public static string GetLocalizedName(string roleName)
    {
        if (string.Equals(SuperAdmin, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مدير النظام";
        }

        if (string.Equals(Admin, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مشرف";
        }

        if (string.Equals(DataAssurance, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مدقق بيانات";
        }

        if (string.Equals(FinancialOperator, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مسؤول مالي";
        }

        if (string.Equals(ExecutionOperator, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مسؤول تنفيذ";
        }

        if (string.Equals(KPIsOperator, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مسؤول مؤشرات أداء";
        }

        if (string.Equals(EntityUser, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مستخدم جهة";
        }

        if (string.Equals(FullAccessViewer, roleName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "مستعرض جميع البيانات";
        }

        return string.Empty;
    }
}