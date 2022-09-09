using Moasher.WebApi.Controllers.Common.ResponseTypes.Abstracts;

namespace Moasher.WebApi.Controllers.Common.ResponseTypes;

public class UnauthorizedResponseType : ResponseTypeBase
{
    public UnauthorizedResponseType() : base(StatusCodes.Status401Unauthorized) { }
}