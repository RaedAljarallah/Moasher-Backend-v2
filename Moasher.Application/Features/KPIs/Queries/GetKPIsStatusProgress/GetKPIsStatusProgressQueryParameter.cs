using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Application.Features.KPIs.Queries.GetKPIsStatusProgress;

public class GetKPIsStatusProgressQueryParameter : IQueryParameterBuilder<KPI>
{
    private readonly GetKPIsStatusProgressQuery _parameter;

    public GetKPIsStatusProgressQueryParameter(GetKPIsStatusProgressQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<KPI> Build(IQueryable<KPI> query)
    {
        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(k => k.EntityId == _parameter.EntityId);
        }
        
        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(k => k.LevelThreeStrategicObjective.Initiatives
                .Select(i => i.ProgramId).Contains(_parameter.ProgramId.Value));
        }
        
        return query;
    }
}
