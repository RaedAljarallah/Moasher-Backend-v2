using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.EditRequests.Queries.GetEditRequestDetails;
using Moasher.Application.Features.EditRequests.Queries.GetEditRequests;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class EditRequestsController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.EditRequests)]
    [HttpGet(ApiEndpoints.EditRequests.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetEditRequestsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }

    [MustHavePermission(Actions.View, Resources.EditRequests)]
    [HttpGet(ApiEndpoints.EditRequests.Details)]
    [UnauthorizedResponseType]
    [NotFoundResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Details(Guid id, [FromRoute] GetEditRequestDetailsQuery query,
        CancellationToken cancellationToken)
    {
        if (!id.Equals(query.Id))
        {
            return NotFound();
        }
        
        return Ok(await Sender.Send(query, cancellationToken));
    }
}