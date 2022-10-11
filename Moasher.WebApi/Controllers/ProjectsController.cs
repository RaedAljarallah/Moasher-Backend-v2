using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Projects.Commands.CreateProject;
using Moasher.Application.Features.Projects.Queries.EditProject;
using Moasher.Application.Features.Projects.Queries.GetProjects;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ProjectsController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Projects.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetProjectsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
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
}
