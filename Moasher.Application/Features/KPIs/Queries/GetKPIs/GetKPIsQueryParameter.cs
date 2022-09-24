using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.KPIEntities;
using System.Linq.Dynamic.Core;
using Moasher.Application.Common.Extensions;

namespace Moasher.Application.Features.KPIs.Queries.GetKPIs;

public class GetKPIsQueryParameter : IQueryParameterBuilder<KPI>
{
    private readonly GetKPIsQuery _parameter;

    public GetKPIsQueryParameter(GetKPIsQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<KPI> Build(IQueryable<KPI> query)
    {
        query = query.OrderBy(_parameter.OrderBy);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery,
                "Name", "Code", "EntityName", "Status_Name",
                "LevelOneStrategicObjectiveName", "LevelTwoStrategicObjectiveName",
                "LevelThreeStrategicObjectiveName", "LevelFourStrategicObjectiveName",
                "OwnerName", "OwnerEmail");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Code))
        {
            query = query.Like(_parameter.Code, "Code");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.EntityName))
        {
            query = query.Like(_parameter.EntityName, "EntityName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.Status))
        {
            query = query.Like(_parameter.Status, "Status_Name");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L1Name))
        {
            query = query.Like(_parameter.L1Name, "LevelOneStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L2Name))
        {
            query = query.Like(_parameter.L2Name, "LevelTwoStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L3Name))
        {
            query = query.Like(_parameter.L3Name, "LevelThreeStrategicObjectiveName");
        }
        
        if (!string.IsNullOrWhiteSpace(_parameter.L4Name))
        {
            query = query.Like(_parameter.L4Name, "LevelFourStrategicObjectiveName");
        }
        
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
            query = query.Where(k => k.LevelThreeStrategicObjective.Initiatives
                .Select(i => i.ProgramId).Contains(_parameter.ProgramId.Value));
        }
        
        if (_parameter.StatusId.HasValue)
        {
            query = query.Where(i => i.StatusEnumId == _parameter.StatusId);
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

        return query;
    }
}