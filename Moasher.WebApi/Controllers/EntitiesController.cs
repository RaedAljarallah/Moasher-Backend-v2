using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Entities.Commands.CreateEntity;
using Moasher.Application.Features.Entities.Commands.DeleteEntity;
using Moasher.Application.Features.Entities.Commands.UpdateEntity;
using Moasher.Application.Features.Entities.Queries.ExportEntities;
using Moasher.Application.Features.Entities.Queries.GetEntities;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;
public class EntitiesController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Entities)]
    [HttpGet(ApiEndpoints.Entities.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetEntitiesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.Entities)]
    [HttpGet(ApiEndpoints.Entities.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportEntitiesQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }

    [MustHavePermission(Actions.Create, Resources.Entities)]
    [HttpPost(ApiEndpoints.Entities.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Entities.All}/{result.Id}", result);
    }
    
    [MustHavePermission(Actions.Update, Resources.Entities)]
    [HttpPut(ApiEndpoints.Entities.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateEntityCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [MustHavePermission(Actions.Delete, Resources.Entities)]
    [HttpDelete(ApiEndpoints.Entities.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteEntityCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}