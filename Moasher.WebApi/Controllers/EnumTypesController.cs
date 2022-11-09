using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.EnumTypes.Commands.CreateEnumType;
using Moasher.Application.Features.EnumTypes.Commands.DeleteEnumType;
using Moasher.Application.Features.EnumTypes.Commands.UpdateEnumType;
using Moasher.Application.Features.EnumTypes.Queries.GetEnumTypes;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class EnumTypesController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.EnumTypes)]
    [HttpGet(ApiEndpoints.EnumTypes.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetEnumTypesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Create, Resources.EnumTypes)]
    [HttpPost(ApiEndpoints.EnumTypes.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateEnumTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.EnumTypes.All}/{result.Id}", result);
    }
    
    [MustHavePermission(Actions.Update, Resources.EnumTypes)]
    [HttpPut(ApiEndpoints.EnumTypes.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateEnumTypeCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [MustHavePermission(Actions.Delete, Resources.EnumTypes)]
    [HttpDelete(ApiEndpoints.EnumTypes.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteEnumTypeCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}