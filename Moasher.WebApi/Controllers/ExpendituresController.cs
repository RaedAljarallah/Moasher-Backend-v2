using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Constants;
using Moasher.Application.Features.Expenditures.Queries.ExportExpenditures;
using Moasher.Application.Features.Expenditures.Queries.GetExpenditures;
using Moasher.WebApi.Attributes;
using Moasher.WebApi.Controllers.Common;
using Moasher.WebApi.Controllers.Common.ResponseTypes;

namespace Moasher.WebApi.Controllers;

public class ExpendituresController : ApiControllerBase
{
    [MustHavePermission(Actions.View, Resources.Expenditures)]
    [HttpGet(ApiEndpoints.Expenditures.All)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("application/json")]
    public async Task<IActionResult> All([FromQuery] GetExpendituresQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(new {result});
    }
    
    [MustHavePermission(Actions.Export, Resources.Expenditures)]
    [HttpGet(ApiEndpoints.Expenditures.Export)]
    [UnauthorizedResponseType]
    [OkResponseType]
    [Produces("text/csv")]
    public async Task<IActionResult> Export([FromQuery] ExportExpendituresQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
}