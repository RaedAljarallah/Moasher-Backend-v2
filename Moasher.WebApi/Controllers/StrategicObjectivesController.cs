using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.StrategicObjectives.Commands.CreateStrategicObjective;
using Moasher.Application.Features.StrategicObjectives.Commands.DeleteStrategicObjective;
using Moasher.Application.Features.StrategicObjectives.Commands.UpdateStrategicObjective;
using Moasher.Application.Features.StrategicObjectives.Queries.ExportStrategicObjectives;
using Moasher.Application.Features.StrategicObjectives.Queries.GetStrategicObjectives;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class StrategicObjectivesController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.StrategicObjectives)]
    [HttpGet(ApiEndpoints.StrategicObjectives.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetStrategicObjectivesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.StrategicObjectives)]
    [HttpGet(ApiEndpoints.StrategicObjectives.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportStrategicObjectivesQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.StrategicObjectives)]
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
    
    [MustHavePermission(Actions.Update, Resources.StrategicObjectives)]
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
    
    [MustHavePermission(Actions.Delete, Resources.StrategicObjectives)]
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