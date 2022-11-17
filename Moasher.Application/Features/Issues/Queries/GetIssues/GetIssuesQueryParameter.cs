using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Issues.Queries.GetIssues;

public class GetIssuesQueryParameter : IQueryParameterBuilder<InitiativeIssue>
{
    private readonly GetIssuesQuery _parameter;

    public GetIssuesQueryParameter(GetIssuesQuery parameter)
    {
        _parameter = parameter;
    }

    public IQueryable<InitiativeIssue> Build(IQueryable<InitiativeIssue> query)
    {
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Description", "ScopeName", "StatusName", "ImpactName",
                "Source", "Reason", "RaisedBy", "InitiativeName");
        }

        if (_parameter.Id.HasValue)
        {
            query = query.Where(i => i.Id == _parameter.Id);
        }

        if (_parameter.InitiativeId.HasValue)
        {
            query = query.Where(i => i.InitiativeId == _parameter.InitiativeId);
        }

        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(i => i.Initiative.EntityId == _parameter.EntityId);
        }

        if (_parameter.ScopeId.HasValue)
        {
            query = query.Where(i => i.ScopeEnumId == _parameter.ScopeId);
        }

        if (_parameter.StatusId.HasValue)
        {
            query = query.Where(i => i.StatusEnumId == _parameter.StatusId);
        }

        if (_parameter.ImpactId.HasValue)
        {
            query = query.Where(i => i.ImpactEnumId == _parameter.ImpactId);
        }

        if (_parameter.RaisedFrom.HasValue)
        {
            query = query.Where(i => i.RaisedAt >= _parameter.RaisedFrom);
        }

        if (_parameter.RaisedTo.HasValue)
        {
            query = query.Where(i => i.RaisedAt <= _parameter.RaisedTo);
        }

        if (_parameter.ClosedFrom.HasValue)
        {
            query = query.Where(i => i.ClosedAt.HasValue)
                .Where(i => i.ClosedAt >= _parameter.ClosedFrom);
        }

        if (_parameter.ClosedTo.HasValue)
        {
            query = query.Where(i => i.ClosedAt.HasValue)
                .Where(i => i.ClosedAt <= _parameter.ClosedTo);
        }

        return query;
    }
}