using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.Expenditures.Queries.GetExpenditures;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ExpendituresController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Expenditures.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetExpendituresQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(new {result});
    }
}