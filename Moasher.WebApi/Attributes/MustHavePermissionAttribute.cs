using Microsoft.AspNetCore.Authorization;
using Moasher.Application.Common.Types;

namespace Moasher.WebApi.Attributes;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource)
    {
        Policy = new Permission(action, resource).ToString();
    }
}