using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Features.SearchRecords.Queries.GetSearchRecords;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class SearchController : ApiControllerBase
{
    [HttpGet(ApiEndpoints.Search.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetSearchRecordsQuery query, CancellationToken cancellationToken)
    {
        return List(await Sender.Send(query, cancellationToken));
    }
}