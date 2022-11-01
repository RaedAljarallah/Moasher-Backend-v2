using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Entities.Commands.CreateEntity;
using Moasher.Application.Features.Entities.Commands.DeleteEntity;
using Moasher.Application.Features.Entities.Commands.UpdateEntity;
using Moasher.Application.Features.Entities.Queries.ExportEntities;
using Moasher.Application.Features.Entities.Queries.GetEntities;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class EntitiesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Entities.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetEntitiesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpGet(ApiEndpoints.Entities.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportEntitiesQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }

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