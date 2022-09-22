using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.ApprovedCosts.Queries.GetApprovedCosts;

public class GetApprovedCostsQueryParameter : IQueryParameterBuilder<InitiativeApprovedCost>
{
    private readonly GetApprovedCostsQuery _parameter;

    public GetApprovedCostsQueryParameter(GetApprovedCostsQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<InitiativeApprovedCost> Build(IQueryable<InitiativeApprovedCost> query)
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