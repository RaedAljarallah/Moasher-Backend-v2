using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Budgets.Queries.GetBudgetsQuery;

public class GetBudgetsQueryParameter : IQueryParameterBuilder<InitiativeBudget>
{
    private readonly GetBudgetsQuery _parameter;

    public GetBudgetsQueryParameter(GetBudgetsQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<InitiativeBudget> Build(IQueryable<InitiativeBudget> query)
    {
        if (_parameter.Id.HasValue)
        {
            query = query.Where(a => a.Id == _parameter.Id);
        }

        if (_parameter.InitiativeId.HasValue)
        {
            query = query.Where(a => a.InitiativeId == _parameter.InitiativeId);
        }

        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(a => a.Initiative.EntityId == _parameter.EntityId);
        }

        if (_parameter.From.HasValue)
        {
            query = query.Where(a => a.ApprovalDate >= _parameter.From);
        }

        if (_parameter.To.HasValue)
        {
            query = query.Where(a => a.ApprovalDate <= _parameter.To);
        }

        return query;
    }
}