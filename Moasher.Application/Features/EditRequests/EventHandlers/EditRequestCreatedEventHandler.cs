using MediatR;
using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Events.EditRequests;

namespace Moasher.Application.Features.EditRequests.EventHandlers;

public class EditRequestCreatedEventHandler : INotificationHandler<EditRequestCreatedEvent>
{
    private readonly IUserNotification _userNotification;
    private readonly IIdentityService _identityService;

    public EditRequestCreatedEventHandler(IUserNotification userNotification, IIdentityService identityService)
    {
        _userNotification = userNotification;
        _identityService = identityService;
    }
    
    public async Task Handle(EditRequestCreatedEvent notification, CancellationToken cancellationToken)
    {
        var editRequest = notification.EditRequest;
        var user = await _identityService.GetUserByEmail(editRequest.RequestedBy, cancellationToken);
        var recipients = await _identityService.GetUsersInRolesAsync(AppRoles.GetDataAssuranceRoles(), cancellationToken);
        if (user is not null && recipients.Any())
        {
            var scopes = string.Join(" - ", editRequest.GetEditScopes().Select(AttributeServices.GetDisplayName<EditRequest>));
            var body = $"يوجد طلب برقم {editRequest.Code} مقد من {user.GetFullName()} بتاريخ {editRequest.RequestedAt:yyyy-MM-dd} لتعديل {scopes}";
            var userNotification = await _userNotification.CreateAsync("طلب تعديل", body, recipients, cancellationToken);
            await _userNotification.NotifyAsync(userNotification, recipients.ToList(), cancellationToken);
        }
        
    }
}