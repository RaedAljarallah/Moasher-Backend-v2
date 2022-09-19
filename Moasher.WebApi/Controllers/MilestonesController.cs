using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Milestones.Commands.CreateMilestone;
using Moasher.Application.Features.Milestones.Queries.GetMilestones;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class MilestonesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Milestones.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetMilestonesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
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
    
    //
    // [HttpPut(ApiEndpoints.Milestones.Update)]
    // [BadRequestResponseType]
    // [UnauthorizedResponseType]
    // [NotFoundResponseType]
    // [OkResponseType]
    // [Produces("application/json")]
    // public async Task<IActionResult> Update(Guid id, UpdateMilestoneCommand command, CancellationToken cancellationToken)
    // {
    //     if (!id.Equals(command.Id))
    //     {
    //         return BadRequest();
    //     }
    //
    //     return Ok(await Sender.Send(command, cancellationToken));
    // }
    //
    // [HttpDelete(ApiEndpoints.Milestones.Delete)]
    // [NotFoundResponseType]
    // [ConflictResponseType]
    // [Produces("application/json")]
    // public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    // {
    //     await Sender.Send(new DeleteMilestoneCommand { Id = id }, cancellationToken);
    //     return NoContent();
    // }
}