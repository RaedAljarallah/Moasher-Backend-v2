using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Application.Features.KPIValues.Queries.GetKPIValues;

public class GetKPIValuesQueryParameter : IQueryParameterBuilder<KPIValue>
{
    private readonly GetKPIValuesQuery _parameter;

    public GetKPIValuesQueryParameter(GetKPIValuesQuery parameter)
    {
        _parameter = parameter;
    }
    public IQueryable<KPIValue> Build(IQueryable<KPIValue> query)
    {
        query = query.WithinSchedulableParameters<KPIValue>(_parameter);
        if (_parameter.Id.HasValue)
        {
            query = query.Where(v => v.Id == _parameter.Id);
        }

        if (_parameter.KPIId.HasValue)
        {
            query = query.Where(v => v.KPIId == _parameter.KPIId);
        }


        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(v => v.KPI.EntityId == _parameter.EntityId);
        }

        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(v => v.KPI.LevelThreeStrategicObjective.Initiatives
                .Select(i => i.ProgramId).Contains(_parameter.ProgramId.Value));
        }
        
        if (_parameter.L1Id.HasValue)
        {
            query = query.Where(v => v.KPI.LevelOneStrategicObjectiveId == _parameter.L1Id);
        }

        if (_parameter.L2Id.HasValue)
        {
            query = query.Where(v => v.KPI.LevelTwoStrategicObjectiveId == _parameter.L2Id);
        }

        if (_parameter.L3Id.HasValue)
        {
            query = query.Where(v => v.KPI.LevelThreeStrategicObjectiveId == _parameter.L3Id);
        }

        if (_parameter.L4Id.HasValue)
        {
            query = query.Where(v => v.KPI.LevelFourStrategicObjectiveId == _parameter.L4Id);
        }

        return query;
    }
}