using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Extensions;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.UserNotifications.Queries;

public class GetUserNotificationsQueryParameter : IQueryParameterBuilder<UserNotification>
{
    private readonly GetUserNotificationsQuery _parameter;

    public GetUserNotificationsQueryParameter(GetUserNotificationsQuery parameter)
    {
        _parameter = parameter;
    }
    
    public IQueryable<UserNotification> Build(IQueryable<UserNotification> query)
    {
        if (!string.IsNullOrWhiteSpace(_parameter.SearchQuery))
        {
            query = query.Like(_parameter.SearchQuery, "Title");
        }

        if (_parameter.Read.HasValue)
        {
            query = query.Where(n => n.HasRead == _parameter.Read.Value);
        }

        return query;
    }
}