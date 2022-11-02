using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Issues.Commands.CreateIssue;
using Moasher.Application.Features.Issues.Commands.DeleteIssue;
using Moasher.Application.Features.Issues.Commands.UpdateIssue;
using Moasher.Application.Features.Issues.Queries.EditIssue;
using Moasher.Application.Features.Issues.Queries.ExportIssues;
using Moasher.Application.Features.Issues.Queries.GetIssues;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class IssuesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Issues.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetIssuesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpGet(ApiEndpoints.Issues.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportIssuesQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [HttpPost(ApiEndpoints.Issues.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateIssueCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Issues.All}/{result.Id}", result);
    }
    
    [HttpGet(ApiEndpoints.Issues.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditIssueQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPut(ApiEndpoints.Issues.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateIssueCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.Issues.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteIssueCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}