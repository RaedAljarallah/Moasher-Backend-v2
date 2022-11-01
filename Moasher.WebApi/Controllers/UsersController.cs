using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Users.Commands.CreateUser;
using Moasher.Application.Features.Users.Commands.DeleteUser;
using Moasher.Application.Features.Users.Commands.ResetUserPassword;
using Moasher.Application.Features.Users.Commands.UpdateUser;
using Moasher.Application.Features.Users.Commands.UpdateUserSuspensionStatus;
using Moasher.Application.Features.Users.Queries.EditUser;
using Moasher.Application.Features.Users.Queries.GetUsers;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Users.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPost(ApiEndpoints.Users.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Users.All}/{result.Id}", result);
    }
    
    [HttpGet(ApiEndpoints.Users.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditUserQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPut(ApiEndpoints.Users.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpPut(ApiEndpoints.Users.UpdateSuspensionStatus)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateSuspension(Guid id, UpdateUserSuspensionStatusCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
        
        return Ok(await Sender.Send(command, cancellationToken));
    }

    [HttpPost(ApiEndpoints.Users.ResetPassword)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [NoContentResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> ResetPassword(Guid id, ResetUserPasswordCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        await Sender.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete(ApiEndpoints.Users.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteUserCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}