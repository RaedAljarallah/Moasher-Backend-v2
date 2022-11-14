using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.UserNotifications.Queries;

public record CreateUserNotificationEmailModel(string Title, string Body) : EmailModelBase;