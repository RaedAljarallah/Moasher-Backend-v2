using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.StrategicObjectives.Commands.CreateStrategicObjective;
using Moasher.Application.Features.StrategicObjectives.Commands.DeleteStrategicObjective;
using Moasher.Application.Features.StrategicObjectives.Commands.UpdateStrategicObjective;
using Moasher.Application.Features.StrategicObjectives.Queries.GetStrategicObjectives;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class StrategicObjectivesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.StrategicObjectives.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetStrategicObjectivesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPost(ApiEndpoints.StrategicObjectives.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateStrategicObjectiveCommand command, CancellationToken cancellationToken)
    {
        dynamic result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.StrategicObjectives.All}/{result.Id}", result);
    }
    
    [HttpPut(ApiEndpoints.StrategicObjectives.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateStrategicObjectiveCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.StrategicObjectives.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteStrategicObjectiveCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}