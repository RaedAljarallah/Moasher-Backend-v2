using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Initiatives.Queries.GetInitiatives;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class InitiativesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Initiatives.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    public async Task<IActionResult> All([FromQuery] GetInitiativesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
}