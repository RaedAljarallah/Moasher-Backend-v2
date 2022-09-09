using Microsoft.AspNetCore.Mvc;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes;

public class NoContentResponseType : ProducesResponseTypeAttribute
{
    public NoContentResponseType() : base(StatusCodes.Status204NoContent)
    {
    }
}