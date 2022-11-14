using MediatR;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Types;

namespace Moasher.Application.Features.Notifications.Queries;

public record GetNotificationsQuery : QueryParameterBase, IRequest<PaginatedList<NotificationDto>>;