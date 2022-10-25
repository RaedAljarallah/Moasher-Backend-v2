using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Milestones.Queries.GetMilestones;

public class GetMilestonesQueryParameter : IQueryParameterBuilder<InitiativeMilestone>
{
    private readonly GetMilestonesQuery _parameter;

    public GetMilestonesQueryParameter(GetMilestonesQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<InitiativeMilestone> Build(IQueryable<InitiativeMilestone> query)
    {
        query = query.WithinSchedulableParameters<InitiativeMilestone>(_parameter);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Name");
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

        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(m => m.Initiative.ProgramId == _parameter.ProgramId);
        }

        if (_parameter.PortfolioId.HasValue)
        {
            query = query.Where(m => m.Initiative.PortfolioId == _parameter.PortfolioId);
        }
        // TODO: Uncomment
        // if (_parameter.ContractId.HasValue)
        // {
        //     query = query.Where(m =>
        //         m.ContractMilestones.Where(cm => cm.ContractId == _parameter.ContractId).Select(cm => cm.MilestoneId).Contains(m.Id));
        // }

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