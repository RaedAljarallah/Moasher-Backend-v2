using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesSummary;

public class GetInitiativesSummaryQueryParameter : IQueryParameterBuilder<Initiative>
{
    private readonly GetInitiativesSummaryQuery _parameter;

    public GetInitiativesSummaryQueryParameter(GetInitiativesSummaryQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<Initiative> Build(IQueryable<Initiative> query)
    {
        if (_parameter.Id.HasValue)
        {
            query = query.Where(i => i.Id == _parameter.Id);
        }

        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(i => i.EntityId == _parameter.EntityId);
        }

        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(i => i.ProgramId == _parameter.ProgramId);
        }
                
        if (_parameter.PortfolioId.HasValue)
        {
            query = query.Where(i => i.PortfolioId == _parameter.PortfolioId);
        }

        if (_parameter.L1Id.HasValue)
        {
            query = query.Where(i => i.LevelOneStrategicObjectiveId == _parameter.L1Id);
        }

        if (_parameter.L2Id.HasValue)
        {
            query = query.Where(i => i.LevelTwoStrategicObjectiveId == _parameter.L2Id);
        }

        if (_parameter.L3Id.HasValue)
        {
            query = query.Where(i => i.LevelThreeStrategicObjectiveId == _parameter.L3Id);
        }

        if (_parameter.L4Id.HasValue)
        {
            query = query.Where(i => i.LevelFourStrategicObjectiveId == _parameter.L4Id);
        }

        if (_parameter.ForDashboard is true)
        {
            query = query.Where(i => i.VisibleOnDashboard);
        }

        return query;
    }
}