using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Entities.Queries.GetEntities;

public class GetEntitiesQueryParameter : IQueryParameterBuilder<Entity>
{
    private readonly GetEntitiesQuery _parameter;

    public GetEntitiesQueryParameter(GetEntitiesQuery parameter)
    {
        _parameter = parameter;
    }

    public IQueryable<Entity> Build(IQueryable<Entity> query)
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

        if (_parameter.Id.HasValue)
        {
            query = query.Where(e => e.Id == _parameter.Id);
        }

        if (_parameter.ProgramId.HasValue)
        {
            query = query.Where(e => e.Initiatives.Select(i => i.ProgramId).Contains(_parameter.ProgramId.Value));
        }

        if (_parameter.WithInitiatives)
        {
            query = query.Include(e => e.Initiatives);
        }

        if (_parameter.WithKPIs)
        {
            query = query.Include(e => e.KPIs);
        }

        if (_parameter.L1Id.HasValue)
        {
            query = query.Where(e =>
                e.Initiatives.Select(i => i.LevelOneStrategicObjectiveId).Contains(_parameter.L1Id.Value)
                || e.KPIs.Select(k => k.LevelOneStrategicObjectiveId).Contains(_parameter.L1Id.Value));
        }

        if (_parameter.L2Id.HasValue)
        {
            query = query.Where(e =>
                e.Initiatives.Select(i => i.LevelTwoStrategicObjectiveId).Contains(_parameter.L2Id.Value)
                || e.KPIs.Select(k => k.LevelTwoStrategicObjectiveId).Contains(_parameter.L2Id.Value));
        }

        if (_parameter.L3Id.HasValue)
        {
            query = query.Where(e =>
                e.Initiatives.Select(i => i.LevelThreeStrategicObjectiveId).Contains(_parameter.L3Id.Value)
                || e.KPIs.Select(k => k.LevelThreeStrategicObjectiveId).Contains(_parameter.L3Id.Value));
        }

        if (_parameter.L4Id.HasValue)
        {
            query = query.Where(e =>
                e.Initiatives.Select(i => i.LevelFourStrategicObjectiveId).Contains(_parameter.L4Id.Value)
                || e.KPIs.Select(k => k.LevelFourStrategicObjectiveId).Contains(_parameter.L4Id.Value));
        }
        
        return query;
    }
}