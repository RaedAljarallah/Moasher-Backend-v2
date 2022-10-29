using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryParameter : IQueryParameterBuilder<User>
{
    private readonly GetUsersQuery _parameter;

    public GetUsersQueryParameter(GetUsersQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<User> Build(IQueryable<User> query)
    {
        query = query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "FirstName", "LastName", "Email");
        }

        if (_parameter.EntityId.HasValue)
        {
            query = query.Where(u => u.EntityId == _parameter.EntityId);
        }

        if (!string.IsNullOrWhiteSpace(_parameter.Role))
        {
            query = query.Where(u => u.Role == _parameter.Role);
        }

        return query;
    }
}