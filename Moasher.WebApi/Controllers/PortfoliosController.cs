using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Portfolios.Commands.CreatePortfolio;
using Moasher.Application.Features.Portfolios.Commands.DeletePortfolio;
using Moasher.Application.Features.Portfolios.Commands.UpdatePortfolio;
using Moasher.Application.Features.Portfolios.Queries.EditPortfolio;
using Moasher.Application.Features.Portfolios.Queries.GetPortfolios;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class PortfoliosController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Portfolios.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetPortfoliosQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [HttpPost(ApiEndpoints.Portfolios.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreatePortfolioCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Portfolios.All}/{result.Id}", result);
    }

    [HttpGet(ApiEndpoints.Portfolios.Edit)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromRoute] EditPortfolioQuery query, CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }

    [HttpPut(ApiEndpoints.Portfolios.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdatePortfolioCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }

        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [HttpDelete(ApiEndpoints.Portfolios.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeletePortfolioCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}