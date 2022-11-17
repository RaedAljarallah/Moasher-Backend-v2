using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Risks.Queries.GetRisks;

public class GetRisksQueryParameter : IQueryParameterBuilder<InitiativeRisk>
{
    private readonly GetRisksQuery _parameter;

    public GetRisksQueryParameter(GetRisksQuery parameter)
    {
        _parameter = parameter;
    }

    public IQueryable<InitiativeRisk> Build(IQueryable<InitiativeRisk> query)
    {
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Description", "Type_Name", "PriorityName", "ProbabilityName",
                "ImpactName", "ScopeName", "Owner", "RaisedBy", "InitiativeName");
        }
        
        if (_parameter.Id.HasValue)
        {
            query = query.Where(r => r.Id == _parameter.Id);
        }

        if (_parameter.InitiativeId.HasValue)
        {
            query = query.Where(r => r.InitiativeId == _parameter.InitiativeId);
        }

        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(r => r.Initiative.EntityId == _parameter.EntityId);
        }

        if (_parameter.TypeId.HasValue)
        {
            query = query.Where(r => r.TypeEnumId == _parameter.TypeId);
        }

        if (_parameter.PriorityId.HasValue)
        {
            query = query.Where(r => r.PriorityEnumId == _parameter.PriorityId);
        }

        if (_parameter.ProbabilityId.HasValue)
        {
            query = query.Where(r => r.ProbabilityEnumId == _parameter.ProbabilityId);
        }

        if (_parameter.ImpactId.HasValue)
        {
            query = query.Where(r => r.ImpactEnumId == _parameter.ImpactId);
        }

        if (_parameter.ScopeId.HasValue)
        {
            query = query.Where(r => r.ScopeEnumId == _parameter.ScopeId);
        }

        if (_parameter.RaisedFrom.HasValue)
        {
            query = query.Where(r => r.RaisedAt >= _parameter.RaisedFrom);
        }

        if (_parameter.RaisedTo.HasValue)
        {
            query = query.Where(r => r.RaisedAt <= _parameter.RaisedTo);
        }

        return query;
    }
}