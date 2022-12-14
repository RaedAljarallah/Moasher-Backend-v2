using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;

namespace Moasher.Application.Features.Programs.Queries.GetPrograms;

public class GetProgramsQueryParameter : IQueryParameterBuilder<Program>
{
    private readonly GetProgramsQuery _parameter;

    public GetProgramsQueryParameter(GetProgramsQuery parameter)
    {
        _parameter = parameter;
    }

    public IQueryable<Program> Build(IQueryable<Program> query)
    {
        query = query.OrderBy(_parameter.OrderBy);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Name", "Code");
        }
        
        if (_parameter.Id.HasValue)
        {
            query = query.Where(e => e.Id == _parameter.Id);
        }

        if (_parameter.WithInitiatives)
        {
            query = query.Include(e => e.Initiatives);
        }

        if (_parameter.WithKPIs)
        {
            query = query.Include(p => p.Initiatives)
                .ThenInclude(i => i.LevelThreeStrategicObjective)
                .ThenInclude(o => o.KPIs);
        }

        if (_parameter.PortfolioId.HasValue)
        {
            query = query.Where(p => p.Initiatives.Select(i => i.PortfolioId).Contains(_parameter.PortfolioId.Value));
        }
        return query;
    }
}