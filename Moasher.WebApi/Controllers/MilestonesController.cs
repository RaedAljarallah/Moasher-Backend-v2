using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Milestones.Commands.CreateMilestone;
using Moasher.Application.Features.Milestones.Commands.DeleteMilestone;
using Moasher.Application.Features.Milestones.Commands.UpdateMilestone;
using Moasher.Application.Features.Milestones.Queries.ExportMilestones;
using Moasher.Application.Features.Milestones.Queries.GetMilestones;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class MilestonesController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Milestones)]
    [HttpGet(ApiEndpoints.Milestones.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetMilestonesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.Milestones)]
    [HttpGet(ApiEndpoints.Milestones.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportMilestonesQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.Milestones)]
    [HttpPost(ApiEndpoints.Milestones.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateMilestoneCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Milestones.All}/{result.Id}", result);
    }
    
    [MustHavePermission(Actions.Update, Resources.Milestones)]
    [HttpPut(ApiEndpoints.Milestones.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateMilestoneCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [MustHavePermission(Actions.Delete, Resources.Milestones)]
    [HttpDelete(ApiEndpoints.Milestones.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteMilestoneCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}