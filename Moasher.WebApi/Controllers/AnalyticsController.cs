using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Analytics.Commands.CreateAnalytic;
using Moasher.Application.Features.Analytics.Commands.DeleteAnalytic;
using Moasher.Application.Features.Analytics.Commands.UpdateAnalytic;
using Moasher.Application.Features.Analytics.Queries.ExportAnalytics;
using Moasher.Application.Features.Analytics.Queries.GetAnalytics;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class AnalyticsController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Analytics.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetAnalyticsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpGet(ApiEndpoints.Analytics.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportAnalyticsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [HttpPost(ApiEndpoints.Analytics.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateAnalyticCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Analytics.All}/{result.Id}", result);
    }
    
    [HttpPut(ApiEndpoints.Analytics.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateAnalyticCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.Analytics.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteAnalyticCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}