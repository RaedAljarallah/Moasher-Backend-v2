using System.Collections.ObjectModel;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Common.Constants;

public static class AppPermissions
{
    public static IReadOnlyList<Permission> SuperAdmin { get; } =
        new ReadOnlyCollection<Permission>(GetSuperAdminPermissions());

    public static IReadOnlyList<Permission> Admin { get; }
        = new ReadOnlyCollection<Permission>(GetAdminPermissions());

    public static IReadOnlyList<Permission> DataAssurance { get; }
        = new ReadOnlyCollection<Permission>(GetDataAssurancePermissions());

    public static IReadOnlyList<Permission> FinancialOperator { get; }
        = new ReadOnlyCollection<Permission>(GetFinancialOperatorPermissions());

    public static IReadOnlyList<Permission> ExecutionOperator { get; }
        = new ReadOnlyCollection<Permission>(GetExecutionOperatorPermissions());

    public static IReadOnlyList<Permission> KPIsOperator { get; }
        = new ReadOnlyCollection<Permission>(GetKPIsOperatorPermissions());

    public static IReadOnlyList<Permission> EntityUser { get; }
        = new ReadOnlyCollection<Permission>(GetEntityUserPermissions());

    public static IReadOnlyList<Permission> FullAccessViewer { get; }
        = new ReadOnlyCollection<Permission>(GetFullAccessViewerPermissions());

    private static Permission[] GetSuperAdminPermissions() => GeneratePermissions().ToArray();

    private static Permission[] GetAdminPermissions()
    {
        return GeneratePermissions()
            .Where(p => p.Resource != Resources.Users)
            .Where(p => p.Resource != Resources.Roles)
            .ToArray();
    }

    private static Permission[] GetDataAssurancePermissions()
    {
        var result = GenerateBasicPermissions();
        result.AddRange(GenerateFromResource(Resources.EditRequests));
        return result.ToArray();
    }

    private static Permission[] GetFinancialOperatorPermissions()
    {
        var result = GenerateBasicPermissions();
        result.AddRange(GenerateFromResource(Resources.ApprovedCosts));
        result.AddRange(GenerateFromResource(Resources.Budgets));
        result.AddRange(GenerateFromResource(Resources.Projects));
        result.AddRange(GenerateFromResource(Resources.Contracts));
        return result.ToArray();
    }

    private static Permission[] GetExecutionOperatorPermissions()
    {
        var result = GenerateBasicPermissions();
        result.AddRange(GenerateFromResource(Resources.Milestones));
        result.AddRange(GenerateFromResource(Resources.Deliverables));
        result.AddRange(GenerateFromResource(Resources.Issues));
        result.AddRange(GenerateFromResource(Resources.Risks));
        result.AddRange(GenerateFromResource(Resources.InitiativeTeams));
        result.AddRange(GenerateFromResource(Resources.Analytics));
        return result.ToArray();
    }

    private static Permission[] GetKPIsOperatorPermissions()
    {
        var result = GenerateBasicPermissions();
        result.AddRange(GenerateFromResource(Resources.KPIValues));
        result.AddRange(GenerateFromResource(Resources.Analytics));
        return result.ToArray();
    }

    private static Permission[] GetEntityUserPermissions()
    {
        var result = GenerateBasicPermissions()
            .Where(p => p.Resource != Resources.Portfolios)
            .Where(p => p.Resource != Resources.Programs)
            .Where(p => p.Resource != Resources.StrategicObjectives)
            .ToList();
        result.AddRange(GenerateFromResource(Resources.Milestones, new[] {Actions.Create, Actions.Delete}));
        result.AddRange(GenerateFromResource(Resources.Deliverables, new[] {Actions.Create, Actions.Delete}));
        result.AddRange(GenerateFromResource(Resources.Issues, new[] {Actions.Update, Actions.Delete}));
        result.AddRange(GenerateFromResource(Resources.Risks, new[] {Actions.Update, Actions.Delete}));
        result.AddRange(GenerateFromResource(Resources.Projects, new[] {Actions.Delete}));
        result.AddRange(GenerateFromResource(Resources.Contracts, new[] {Actions.Delete}));
        result.AddRange(GenerateFromResource(Resources.KPIValues, new[] {Actions.Create, Actions.Delete}));
        result.Add(new Permission(Actions.View, Resources.Programs));
        result.Add(new Permission(Actions.View, Resources.StrategicObjectives));
        return result.ToArray();
    }

    private static Permission[] GetFullAccessViewerPermissions()
    {
        return GenerateBasicPermissions()
            .Where(p => p.Resource != Resources.EditRequests)
            .ToArray();
    }

    private static List<Permission> GenerateBasicPermissions()
    {
        var result = GeneratePermissions()
            .Where(p => p.Resource != Resources.Users)
            .Where(p => p.Resource != Resources.Roles)
            .Where(p => p.Resource != Resources.EnumTypes)
            .Where(p => p.Action != Actions.Create)
            .Where(p => p.Action != Actions.Delete)
            .Where(p => p.Action != Actions.Update)
            .ToList();
        result.Add(new Permission(Actions.View, Resources.EnumTypes));

        return result;
    }

    private static IEnumerable<Permission> GenerateFromResource(string resource)
    {
        return Actions.All.Select(action => new Permission(action, resource)).ToList();
    }

    private static IEnumerable<Permission> GenerateFromResource(string resource, string[] excludedActions)
    {
        return Actions.All
            .Where(a => !excludedActions.Contains(a))
            .Select(action => new Permission(action, resource))
            .ToList();
    }

    private static List<Permission> GenerateFromAction(string action)
    {
        return Resources.All.Select(resource => new Permission(action, resource)).ToList();
    }

    private static List<Permission> GeneratePermissions()
    {
        return Resources.All
            .SelectMany(_ => Actions.All, (resource, action) => new Permission(action, resource))
            .ToList();
    }
}