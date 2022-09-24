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
            query = query.Like(_parameter.SearchQuery, "Description", "Type_Name", "Priority_Name", "Probability_Name",
                "Impact_Name", "Scope_Name", "Owner", "RaisedBy", "InitiativeName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Description))
        {
            query = query.Like(_parameter.Description, "Description");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Type))
        {
            query = query.Like(_parameter.Type, "Type_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Priority))
        {
            query = query.Like(_parameter.Priority, "Priority_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Probability))
        {
            query = query.Like(_parameter.Probability, "Probability_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Impact))
        {
            query = query.Like(_parameter.Impact, "Impact_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Scope))
        {
            query = query.Like(_parameter.Scope, "Scope_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Owner))
        {
            query = query.Like(_parameter.Owner, "Owner");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.RaisedBy))
        {
            query = query.Like(_parameter.RaisedBy, "RaisedBy");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.InitiativeName))
        {
            query = query.Like(_parameter.InitiativeName, "InitiativeName");
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