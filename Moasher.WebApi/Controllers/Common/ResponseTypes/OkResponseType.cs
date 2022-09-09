using Microsoft.AspNetCore.Mvc;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes;

public class OkResponseType : ProducesResponseTypeAttribute
{
    public OkResponseType() : base(StatusCodes.Status200OK) { }
}