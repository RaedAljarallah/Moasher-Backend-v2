using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.ApprovedCosts.Commands.CreateApprovedCost;
using Moasher.Application.Features.ApprovedCosts.Commands.DeleteApprovedCost;
using Moasher.Application.Features.ApprovedCosts.Commands.UpdateApprovedCost;
using Moasher.Application.Features.ApprovedCosts.Queries.ExportApprovedCosts;
using Moasher.Application.Features.ApprovedCosts.Queries.GetApprovedCosts;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ApprovedCostsController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.ApprovedCosts)]
    [HttpGet(ApiEndpoints.ApprovedCosts.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetApprovedCostsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.ApprovedCosts)]
    [HttpGet(ApiEndpoints.ApprovedCosts.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportApprovedCostsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.ApprovedCosts)]
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
    
    [MustHavePermission(Actions.Update, Resources.ApprovedCosts)]
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
    
    [MustHavePermission(Actions.Delete, Resources.ApprovedCosts)]
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