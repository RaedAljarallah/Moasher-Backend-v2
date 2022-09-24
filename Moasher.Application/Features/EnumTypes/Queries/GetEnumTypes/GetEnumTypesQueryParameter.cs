using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Entities;
using System.Linq.Dynamic.Core;
using Moasher.Application.Common.Extensions;

namespace Moasher.Application.Features.EnumTypes.Queries.GetEnumTypes;

public class GetEnumTypesQueryParameter : IQueryParameterBuilder<EnumType>
{
    private readonly GetEnumTypesQuery _parameter;

    public GetEnumTypesQueryParameter(GetEnumTypesQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<EnumType> Build(IQueryable<EnumType> query)
    {
        query = query.OrderBy(_parameter.OrderBy);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Name");
        }
        if (!string.IsNullOrWhiteSpace(_parameter.Name))
        {
            query = query.Like(_parameter.Name, "Name");
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Category))
        {
            query = query.Where(e => e.Category == _parameter.Category);
        }
        
        if (_parameter.Id.HasValue)
        {
            query = query.Where(e => e.Id == _parameter.Id);
        }

        return query;
    }
}