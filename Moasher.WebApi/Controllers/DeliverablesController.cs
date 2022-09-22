using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Deliverables.Commands.CreateDeliverable;
using Moasher.Application.Features.Deliverables.Commands.DeleteDeliverable;
using Moasher.Application.Features.Deliverables.Commands.UpdateDeliverable;
using Moasher.Application.Features.Deliverables.Queries.GetDeliverables;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class DeliverablesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Deliverables.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetDeliverablesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPost(ApiEndpoints.Deliverables.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateDeliverableCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Deliverables.All}/{result.Id}", result);
    }
    
    [HttpPut(ApiEndpoints.Deliverables.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateDeliverableCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.Deliverables.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteDeliverableCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}