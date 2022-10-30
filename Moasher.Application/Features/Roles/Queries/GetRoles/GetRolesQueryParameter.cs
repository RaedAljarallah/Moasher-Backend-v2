using System.Linq.Dynamic.Core;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQueryParameter : IQueryParameterBuilder<Role>
{
    private readonly GetRolesQuery _parameter;

    public GetRolesQueryParameter(GetRolesQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<Role> Build(IQueryable<Role> query)
    {
        query = query.OrderBy(r => r.Name);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "LocalizedName");
        }

        return query;
    }
}