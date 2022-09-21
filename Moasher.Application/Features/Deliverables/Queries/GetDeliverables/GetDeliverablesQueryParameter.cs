using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Deliverables.Queries.GetDeliverables;

public class GetDeliverablesQueryParameter : IQueryParameterBuilder<InitiativeDeliverable>
{
    private readonly GetDeliverablesQuery _parameter;

    public GetDeliverablesQueryParameter(GetDeliverablesQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<InitiativeDeliverable> Build(IQueryable<InitiativeDeliverable> query)
    {
        query = query.WithinSchedulableParameters<InitiativeDeliverable>(_parameter);
        if (!string.IsNullOrWhiteSpace(_parameter.Q))
        {
            query = query.Like(_parameter.Q, "Name");
        }
        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }
        
        if (_parameter.Id.HasValue)
        {
            query = query.Where(m => m.Id == _parameter.Id);
        }
        if (_parameter.InitiativeId.HasValue)
        {
            query = query.Where(m => m.InitiativeId == _parameter.InitiativeId);
        }

        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(m => m.Initiative.EntityId == _parameter.EntityId);
        }
        
        if (_parameter.L1Id.HasValue)
        {
            query = query.Where(m => m.Initiative.LevelOneStrategicObjectiveId == _parameter.L1Id);
        }

        if (_parameter.L2Id.HasValue)
        {
            query = query.Where(m => m.Initiative.LevelTwoStrategicObjectiveId == _parameter.L2Id);
        }

        if (_parameter.L3Id.HasValue)
        {
            query = query.Where(m => m.Initiative.LevelThreeStrategicObjectiveId == _parameter.L3Id);
        }

        if (_parameter.L4Id.HasValue)
        {
            query = query.Where(m => m.Initiative.LevelFourStrategicObjectiveId == _parameter.L4Id);
        }

        if (_parameter.ForDashboard is true)
        {
            query = query.Where(i => i.Initiative.VisibleOnDashboard);
        }

        return query;
        
    }
}