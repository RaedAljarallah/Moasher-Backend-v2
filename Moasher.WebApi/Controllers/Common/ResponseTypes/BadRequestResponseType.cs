using Microsoft.AspNetCore.Mvc;
using Moasher.WebApi.Controllers.Common.ResponseTypes.Abstracts;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes;

public class BadRequestResponseType : ProducesResponseTypeAttribute
{
    public BadRequestResponseType() : base(typeof(ValidationErrorResponseSchema), StatusCodes.Status400BadRequest)
    {
    }
}