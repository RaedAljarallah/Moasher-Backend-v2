using Microsoft.AspNetCore.Mvc;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes;

public class ConflictResponseType : ProducesResponseTypeAttribute
{
    public ConflictResponseType() : base(StatusCodes.Status409Conflict)
    {
    }
}