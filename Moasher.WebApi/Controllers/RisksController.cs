using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Risks.Commands.CreateRisk;
using Moasher.Application.Features.Risks.Commands.DeleteRisk;
using Moasher.Application.Features.Risks.Commands.UpdateRisk;
using Moasher.Application.Features.Risks.Queries.EditRisk;
using Moasher.Application.Features.Risks.Queries.ExportRisks;
using Moasher.Application.Features.Risks.Queries.GetRisks;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class RisksController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Risks)]
    [HttpGet(ApiEndpoints.Risks.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetRisksQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.Risks)]
    [HttpGet(ApiEndpoints.Risks.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportRisksQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.Risks)]
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
    
    [MustHavePermission(Actions.Update, Resources.Risks)]
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
    
    [MustHavePermission(Actions.Update, Resources.Risks)]
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
    
    [MustHavePermission(Actions.Delete, Resources.Risks)]
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