using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Initiatives.Commands.CreateInitiative;
using Moasher.Application.Features.Initiatives.Commands.DeleteInitiative;
using Moasher.Application.Features.Initiatives.Commands.UpdateInitiative;
using Moasher.Application.Features.Initiatives.Queries.EditInitiative;
using Moasher.Application.Features.Initiatives.Queries.GetInitiativeDetails;
using Moasher.Application.Features.Initiatives.Queries.GetInitiatives;
using Moasher.Application.Features.Initiatives.Queries.GetInitiativesProgress;
using Moasher.Application.Features.Initiatives.Queries.GetInitiativesStatusProgress;
using Moasher.Application.Features.Initiatives.Queries.GetInitiativesSummary;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class InitiativesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Initiatives.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetInitiativesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpGet(ApiEndpoints.Initiatives.Progress)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetInitiativesProgressQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(new {result});
    }
    
    [HttpGet(ApiEndpoints.Initiatives.StatusProgress)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetInitiativesStatusProgressQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(new {result});
    }
    
    [HttpGet(ApiEndpoints.Initiatives.Summary)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Summary([FromQuery] GetInitiativesSummaryQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [HttpGet(ApiEndpoints.Initiatives.Details)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [NotFoundResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Details(Guid id, [FromRoute] GetInitiativeDetailsQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPost(ApiEndpoints.Initiatives.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateInitiativeCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Initiatives.All}/{result.Id}", result);
    }

    [HttpGet(ApiEndpoints.Initiatives.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditInitiativeQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }

    [HttpPut(ApiEndpoints.Initiatives.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateInitiativeCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.Initiatives.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteInitiativeCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}