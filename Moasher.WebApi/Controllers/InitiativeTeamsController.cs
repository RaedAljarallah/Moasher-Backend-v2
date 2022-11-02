using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.InitiativeTeams.Commands.CreateInitiativeTeam;
using Moasher.Application.Features.InitiativeTeams.Commands.DeleteInitiativeTeam;
using Moasher.Application.Features.InitiativeTeams.Commands.UpdateInitiativeTeam;
using Moasher.Application.Features.InitiativeTeams.Queries.EditInitiativeTeam;
using Moasher.Application.Features.InitiativeTeams.Queries.ExportInitiativeTeams;
using Moasher.Application.Features.InitiativeTeams.Queries.GetInitiativeTeams;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class InitiativeTeamsController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.InitiativeTeams.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetInitiativeTeamsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpGet(ApiEndpoints.InitiativeTeams.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportInitiativeTeamsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [HttpPost(ApiEndpoints.InitiativeTeams.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateInitiativeTeamCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.InitiativeTeams.All}/{result.Id}", result);
    }
    
    [HttpGet(ApiEndpoints.InitiativeTeams.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditInitiativeTeamQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPut(ApiEndpoints.InitiativeTeams.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateInitiativeTeamCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.InitiativeTeams.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteInitiativeTeamCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}