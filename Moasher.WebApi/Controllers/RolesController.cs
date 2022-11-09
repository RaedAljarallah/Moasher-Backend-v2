using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Roles.Queries.GetRoles;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

[Authorize(Policy = "SuperAdminAccess")]
public class RolesController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Roles.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetRolesQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
}