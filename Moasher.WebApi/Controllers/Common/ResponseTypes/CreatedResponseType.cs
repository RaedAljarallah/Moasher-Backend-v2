using Microsoft.AspNetCore.Mvc;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes;

public class CreatedResponseType : ProducesResponseTypeAttribute
{
    public CreatedResponseType() : base(StatusCodes.Status201Created)
    {
    }
}