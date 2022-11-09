using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Budgets.Commands.CreateBudget;
using Moasher.Application.Features.Budgets.Commands.DeleteBudget;
using Moasher.Application.Features.Budgets.Commands.UpdateBudget;
using Moasher.Application.Features.Budgets.Queries.ExportBudgets;
using Moasher.Application.Features.Budgets.Queries.GetBudgetsQuery;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class BudgetsController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Budgets)]
    [HttpGet(ApiEndpoints.Budgets.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetBudgetsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
    
    [MustHavePermission(Actions.Export, Resources.Budgets)]
    [HttpGet(ApiEndpoints.Budgets.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportBudgetsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
    
    [MustHavePermission(Actions.Create, Resources.Budgets)]
    [HttpPost(ApiEndpoints.Budgets.Create)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [CreatedResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateBudgetCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return Created($"{ApiEndpoints.Budgets.All}/{result.Id}", result);
    }
    
    [MustHavePermission(Actions.Update, Resources.Budgets)]
    [HttpPut(ApiEndpoints.Budgets.Update)]
    [BadRequestResponseType]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Guid id, UpdateBudgetCommand command, CancellationToken cancellationToken)
    {
        if (!id.Equals(command.Id))
        {
            return BadRequest();
        }
    
        return Ok(await Sender.Send(command, cancellationToken));
    }
    
    [MustHavePermission(Actions.Delete, Resources.Budgets)]
    [HttpDelete(ApiEndpoints.Budgets.Delete)]
    [NotFoundResponseType]
    [ConflictResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteBudgetCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}