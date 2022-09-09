using Microsoft.AspNetCore.Mvc;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes;

public class NotFoundResponseType : ProducesResponseTypeAttribute
{
    public NotFoundResponseType() : base(StatusCodes.Status404NotFound)
    {
    }
}