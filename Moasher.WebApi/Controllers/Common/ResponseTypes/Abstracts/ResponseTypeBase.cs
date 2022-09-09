using Microsoft.AspNetCore.Mvc;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes.Abstracts;

public abstract class ResponseTypeBase : ProducesResponseTypeAttribute
{
    protected ResponseTypeBase(int statusCode)
        : base(typeof(ErrorResponseSchema), statusCode) { }
}