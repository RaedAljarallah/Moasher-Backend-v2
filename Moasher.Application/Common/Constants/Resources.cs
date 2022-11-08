using System.Collections.ObjectModel;

namespace Moasher.Application.Common.Constants;

public struct Resources
{
    public const string Entities = nameof(Entities);
    public const string Initiatives = nameof(Initiatives);
    public const string KPIs = nameof(KPIs);
    public const string KPIValues = nameof(KPIValues);
    public const string Portfolios = nameof(Portfolios);
    public const string Programs = nameof(Programs);
    public const string StrategicObjectives = nameof(StrategicObjectives);
    public const string EnumTypes = nameof(EnumTypes);
    public const string Milestones = nameof(Milestones);
    public const string Deliverables = nameof(Deliverables);
    public const string ApprovedCosts = nameof(ApprovedCosts);
    public const string Budgets = nameof(Budgets);
    public const string Issues = nameof(Issues);
    public const string Risks = nameof(Risks);
    public const string Projects = nameof(Projects);
    public const string Contracts = nameof(Contracts);
    public const string Expenditures = nameof(Expenditures);
    public const string InitiativeTeams = nameof(InitiativeTeams);
    public const string Analytics = nameof(Analytics);
    public const string Users = nameof(Users);
    public const string Roles = nameof(Roles);
    public const string Search = nameof(Search);
    public const string InvalidTokens = nameof(InvalidTokens);

    public static IReadOnlyList<string> All = new ReadOnlyCollection<string>(new[]
    {
        Entities,
        Initiatives,
        KPIs,
        KPIValues,
        Portfolios,
        Programs,
        StrategicObjectives,
        EnumTypes,
        Milestones,
        Deliverables,
        ApprovedCosts,
        Budgets,
        Issues,
        Risks,
        Projects,
        Contracts,
        Expenditures,
        InitiativeTeams,
        Analytics,
        Users,
        Roles,
        Search,
        InvalidTokens
    });
}