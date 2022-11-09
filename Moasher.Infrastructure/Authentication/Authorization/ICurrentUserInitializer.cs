using System.Security.Claims;

namespace Moasher.Infrastructure.Authentication.Authorization;

public interface ICurrentUserInitializer
{
    public void SetCurrentUser(ClaimsPrincipal user);
}