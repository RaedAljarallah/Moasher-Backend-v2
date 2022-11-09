﻿using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moasher.Authentication.Core.Identity.Entities;

namespace Moasher.Authentication.Core.Identity.Configurations;

public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
{
    public ApplicationClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) :
        base(userManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        var claims = new List<Claim>
        {
            new(JwtClaimTypes.Name, user.GetFullName()),
            new(JwtClaimTypes.Role, user.Role),
            new(JwtClaimTypes.Email, user.Email)
        };
        
        identity.AddClaims(claims);
        return identity;
    }
}