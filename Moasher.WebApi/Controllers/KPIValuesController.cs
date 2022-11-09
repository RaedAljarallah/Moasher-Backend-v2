using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.KPIValues.Commands.CreateKPIValue;
using Moasher.Application.Features.KPIValues.Commands.DeleteKPIValue;
using Moasher.Application.Features.KPIValues.Commands.UpdateKPIValue;
using Moasher.Application.Features.KPIValues.Queries.ExportKPIsValues;
using Moasher.Application.Features.KPIValues.Queries.GetKPIValues;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class KPIValuesController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.KPIValues)]
    [HttpGet(ApiEndpoints.KPIValues.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetKPIValuesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.KPIValues)]
    [HttpGet(ApiEndpoints.KPIValues.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportKPIsValuesQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.KPIValues)]
    [HttpPost(ApiEndpoints.KPIValues.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateKPIValueCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.KPIValues.All}/{result.Id}", result);
    }
    
    [MustHavePermission(Actions.Update, Resources.KPIValues)]
    [HttpPut(ApiEndpoints.KPIValues.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateKPIValueCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    
    [MustHavePermission(Actions.Delete, Resources.KPIValues)]
    [HttpDelete(ApiEndpoints.KPIValues.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteKPIValueCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}