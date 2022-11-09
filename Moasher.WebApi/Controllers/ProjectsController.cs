using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Projects.Commands.CreateProject;
using Moasher.Application.Features.Projects.Commands.DeleteProject;
using Moasher.Application.Features.Projects.Commands.UpdateProject;
using Moasher.Application.Features.Projects.Queries.EditProject;
using Moasher.Application.Features.Projects.Queries.ExportProjects;
using Moasher.Application.Features.Projects.Queries.GetProjects;
using Moasher.Application.Features.Projects.Queries.GetProjectsSummary;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ProjectsController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Projects)]
    [HttpGet(ApiEndpoints.Projects.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetProjectsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.Projects)]
    [HttpGet(ApiEndpoints.Projects.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportProjectsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.View, Resources.Projects)]
    [HttpGet(ApiEndpoints.Projects.Summary)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetProjectsSummaryQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(new {result});
    }
    
    [MustHavePermission(Actions.Create, Resources.Projects)]
    [HttpPost(ApiEndpoints.Projects.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Projects.All}/{result.Id}", result);
    }
    
    [MustHavePermission(Actions.Update, Resources.Projects)]
    [HttpGet(ApiEndpoints.Projects.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditProjectQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Update, Resources.Projects)]
    [HttpPut(ApiEndpoints.Projects.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [MustHavePermission(Actions.Delete, Resources.Projects)]
    [HttpDelete(ApiEndpoints.Projects.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteProjectCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}
