using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Programs.Commands.CreateProgram;
using Moasher.Application.Features.Programs.Commands.DeleteProgram;
using Moasher.Application.Features.Programs.Commands.UpdateProgram;
using Moasher.Application.Features.Programs.Queries.ExportPrograms;
using Moasher.Application.Features.Programs.Queries.GetPrograms;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ProgramsController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Programs.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetProgramsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpGet(ApiEndpoints.Programs.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportProgramsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [HttpPost(ApiEndpoints.Programs.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateProgramCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Programs.All}/{result.Id}", result);
    }
    
    [HttpPut(ApiEndpoints.Programs.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateProgramCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.Programs.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteProgramCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}