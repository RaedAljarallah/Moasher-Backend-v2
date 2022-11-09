using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.KPIs.Commands.CreateKPI;
using Moasher.Application.Features.KPIs.Commands.DeleteKPI;
using Moasher.Application.Features.KPIs.Commands.UpdateKPI;
using Moasher.Application.Features.KPIs.Queries.EditKPI;
using Moasher.Application.Features.KPIs.Queries.ExportKPIs;
using Moasher.Application.Features.KPIs.Queries.GetKPIProgress;
using Moasher.Application.Features.KPIs.Queries.GetKPIs;
using Moasher.Application.Features.KPIs.Queries.GetKPIsStatusProgress;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class KPIsController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.KPIs)]
    [HttpGet(ApiEndpoints.KPIs.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetKPIsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.KPIs)]
    [HttpGet(ApiEndpoints.KPIs.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportKPIsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.View, Resources.KPIs)]
    [HttpGet(ApiEndpoints.KPIs.Progress)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Progress([FromQuery] GetKPIProgressQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(new {result});
    }
    
    [MustHavePermission(Actions.View, Resources.KPIs)]
    [HttpGet(ApiEndpoints.KPIs.StatusProgress)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> StatusProgress([FromQuery] GetKPIsStatusProgressQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(new {result});
    }
    
    [MustHavePermission(Actions.Create, Resources.KPIs)]
    [HttpPost(ApiEndpoints.KPIs.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateKPICommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.KPIs.All}/{result.Id}", result);
    }

    [MustHavePermission(Actions.Update, Resources.KPIs)]
    [HttpGet(ApiEndpoints.KPIs.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditKPIQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }

    [MustHavePermission(Actions.Update, Resources.KPIs)]
    [HttpPut(ApiEndpoints.KPIs.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateKPICommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [MustHavePermission(Actions.Delete, Resources.KPIs)]
    [HttpDelete(ApiEndpoints.KPIs.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteKPICommand { Id = id }, cancellationToken);
        return NoContent();
    }
}