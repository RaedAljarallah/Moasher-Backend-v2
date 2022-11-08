using System.Collections.ObjectModel;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Common.Constants;

public static class AppPermissions
{
    public static IReadOnlyList<Permission> All = new ReadOnlyCollection<Permission>(GenerateFromResourcesAndActions());
    public static IReadOnlyList<Permission> Admin = new ReadOnlyCollection<Permission>(GenerateFromResourcesAndActions(new []
    {
        Resources.Users
    }));

    private static Permission[] GenerateFromResourcesAndActions(string[]? excludedResources = null)
    {
        return GeneratePermissions(excludedResources);
    }
    
    private static Permission[] GeneratePermissions(string[]? excludedResources = null, string[]? excludedActions = null)
    {
        return (
            from resource in Resources.All.Except(excludedResources ?? Array.Empty<string>())
            from action in Actions.All.Except(excludedActions ?? Array.Empty<string>())
            select new Permission(action, resource)
        ).ToArray();
    }
}