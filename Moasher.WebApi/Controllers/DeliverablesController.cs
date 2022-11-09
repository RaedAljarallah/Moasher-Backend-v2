using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Deliverables.Commands.CreateDeliverable;
using Moasher.Application.Features.Deliverables.Commands.DeleteDeliverable;
using Moasher.Application.Features.Deliverables.Commands.UpdateDeliverable;
using Moasher.Application.Features.Deliverables.Queries.ExportDeliverables;
using Moasher.Application.Features.Deliverables.Queries.GetDeliverables;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class DeliverablesController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Deliverables)]
    [HttpGet(ApiEndpoints.Deliverables.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetDeliverablesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.Deliverables)]
    [HttpGet(ApiEndpoints.Deliverables.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportDeliverablesQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.Deliverables)]
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
    
    
    [MustHavePermission(Actions.Update, Resources.Deliverables)]
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
    
    [MustHavePermission(Actions.Delete, Resources.Deliverables)]
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