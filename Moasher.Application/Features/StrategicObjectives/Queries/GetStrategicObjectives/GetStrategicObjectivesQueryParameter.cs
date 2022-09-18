using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities.StrategicObjectiveEntities;
using System.Linq.Dynamic.Core;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Services;

namespace Moasher.Application.Features.StrategicObjectives.Queries.GetStrategicObjectives;

public class GetStrategicObjectivesQueryParameter : IQueryParameterBuilder<StrategicObjective>
{
    private readonly GetStrategicObjectivesQuery _parameter;

    public GetStrategicObjectivesQueryParameter(GetStrategicObjectivesQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<StrategicObjective> Build(IQueryable<StrategicObjective> query)
    {
        query = query.OrderBy(_parameter.OrderBy);
        if (!string.IsNullOrWhiteSpace(_parameter.Q))
        {
            query = query.Like(_parameter.Q, "Name", "Code");
        }
        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Code))
        {
            query = query.Like(_parameter.Code, "Code");
        }
        
        query = query.Where(o => o.HierarchyId.GetLevel() == _parameter.Level);
        
        if (!string.IsNullOrWhiteSpace(_parameter.DescendantOf) && HierarchyIdService.IsValidHierarchyId(_parameter.DescendantOf))
        {
            query = query.Where(o => o.HierarchyId.IsDescendantOf(HierarchyIdService.Parse(_parameter.DescendantOf)));
        }
        
        if (_parameter.Id.HasValue)
        {
            query = query.Where(o => o.Id == _parameter.Id);
        }
        
        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(o =>
                o.Initiatives.Select(i => i.EntityId).Contains(_parameter.EntityId.Value)
                || o.KPIs.Select(k => k.EntityId).Contains(_parameter.EntityId.Value));
        }
        
        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(o => o.Initiatives.Select(i => i.ProgramId).Contains(_parameter.ProgramId.Value));
        }

        return query;
    }
}