using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moasher.Application.Common.Types;

namespace Moasher.WebApi.Controllers.Common;

[ApiController]
[Route("api/v{version:apiVersion}")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ApiScope")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected string GetIpAddress()
    {
        return Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"]
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
    }

    protected IActionResult List<TResponse>(TResponse response)
    {
        if (response is not IPaginatedList paginatedResult)
        {
            return Ok(response);
        }

        var result = new
        {
            result = response,
            pagination = new
            {
                currentPage = paginatedResult.CurrentPage,
                totalPages = paginatedResult.TotalPages,
                pageSize = paginatedResult.PageSize,
                totalCount = paginatedResult.TotalCount,
                hasPreviousPage = paginatedResult.HasPreviousPage,
                hasNextPage = paginatedResult.HasNextPage
            }
        };
        return Ok(result);

    }
}