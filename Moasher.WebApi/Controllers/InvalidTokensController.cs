using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.InvalidTokens.Commands.CreateInvalidToken;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class InvalidTokensController : ApiControllerBase
{
    [HttpPost(ApiEndpoints.InvalidTokens.Create)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateInvalidTokenCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Sender.Send(command, cancellationToken));
    }
}