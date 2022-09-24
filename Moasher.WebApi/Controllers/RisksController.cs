using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Risks.Commands.CreateRisk;
using Moasher.Application.Features.Risks.Commands.DeleteRisk;
using Moasher.Application.Features.Risks.Commands.UpdateRisk;
using Moasher.Application.Features.Risks.Queries.EditRisk;
using Moasher.Application.Features.Risks.Queries.GetRisks;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class RisksController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Risks.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetRisksQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPost(ApiEndpoints.Risks.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateRiskCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Risks.All}/{result.Id}", result);
    }
    
    [HttpGet(ApiEndpoints.Risks.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditRiskQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPut(ApiEndpoints.Risks.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateRiskCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.Risks.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteRiskCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}