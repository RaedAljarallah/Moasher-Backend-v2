using Microsoft.AspNetCore.Authentication;

namespace Moasher.Authentication.Core.Common.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// Determines if the authentication scheme support sign-out.
    /// </summary>
    public static async Task<bool> CustomGetSchemeSupportsSignOutAsync(this HttpContext context, string scheme)
    {
        var provider = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
        var handler = await provider.GetHandlerAsync(context, scheme);
        return (handler is IAuthenticationSignOutHandler);
    }
}