using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Contracts.Commands.CreateContract;
using Moasher.Application.Features.Contracts.Commands.DeleteContract;
using Moasher.Application.Features.Contracts.Commands.UpdateContract;
using Moasher.Application.Features.Contracts.Queries.EditContract;
using Moasher.Application.Features.Contracts.Queries.ExportContracts;
using Moasher.Application.Features.Contracts.Queries.GetContracts;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ContractsController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Contracts)]
    [HttpGet(ApiEndpoints.Contracts.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetContractsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.Contracts)]
    [HttpGet(ApiEndpoints.Contracts.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportContractsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.Contracts)]
    [HttpPost(ApiEndpoints.Contracts.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateContractCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Contracts.All}/{result.Id}", result);
    }
    
    [MustHavePermission(Actions.Update, Resources.Contracts)]
    [HttpGet(ApiEndpoints.Contracts.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditContractQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Update, Resources.Contracts)]
    [HttpPut(ApiEndpoints.Contracts.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateContractCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [MustHavePermission(Actions.Delete, Resources.Contracts)]
    [HttpDelete(ApiEndpoints.Contracts.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteContractCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}