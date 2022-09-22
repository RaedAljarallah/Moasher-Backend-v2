using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.ApprovedCosts.Commands.CreateApprovedCost;
using Moasher.Application.Features.ApprovedCosts.Commands.DeleteApprovedCost;
using Moasher.Application.Features.ApprovedCosts.Commands.UpdateApprovedCost;
using Moasher.Application.Features.ApprovedCosts.Queries.GetApprovedCosts;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ApprovedCostsController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.ApprovedCosts.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetApprovedCostsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPost(ApiEndpoints.ApprovedCosts.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateApprovedCostCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.ApprovedCosts.All}/{result.Id}", result);
    }
    
    [HttpPut(ApiEndpoints.ApprovedCosts.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateApprovedCostCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.ApprovedCosts.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteApprovedCostCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}