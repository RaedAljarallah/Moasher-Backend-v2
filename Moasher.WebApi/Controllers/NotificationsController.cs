using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.UserNotifications.Queries;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class NotificationsController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Notifications.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetUserNotificationsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    // [HttpPut(ApiEndpoints.Notifications.Read)]
    // [BadRequestResponseType]
    // [UnauthorizedResponseType]
    // [NotFoundResponseType]
    // [OkResponseType]
    // [Produces("application/json")]
    // public async Task<IActionResult> Update(Guid id, UpdateUserNotificationCommand command, CancellationToken cancellationToken)
    // {
    //     if (!id.Equals(command.Id))
    //     {
    //         return BadRequest();
    //     }
    //
    //     return Ok(await Sender.Send(command, cancellationToken));
    // }
    
    // [HttpDelete(ApiEndpoints.Notifications.Delete)]
    // [NotFoundResponseType]
    // [ConflictResponseType]
    // [Produces("application/json")]
    // public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    // {
    //     await Sender.Send(new DeleteUserNotificationCommand { Id = id }, cancellationToken);
    //     return NoContent();
    // }
}