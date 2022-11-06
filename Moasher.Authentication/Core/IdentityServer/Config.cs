﻿using IdentityServer4;
using IdentityServer4.Models;

namespace Moasher.Authentication.Core.IdentityServer;

internal static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("access_as_user")
        };
    
    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("web_app", "Web App")
            {
                Scopes = { "access_as_user" },
                UserClaims = { "role" },
                Description = "Web app using Moasher APIs"
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientName = "web_app",
                ClientId = "16e6bea1-b75e-4e6f-9b76-0c5dd11b2e2d",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = { new Secret("v~Zju4vHoJIrvJ#zBb/R;EL+)NjXB?fN`nsHP0VOu&6.ZCozY%P,85`W[V|n?SU".Sha256()) },
                RequirePkce = true,
                RequireConsent = false,
                AllowedCorsOrigins = { "http://localhost:4200", "https://localhost:4200" },
                RedirectUris = { "http://localhost:4200/login-callback", "https://localhost:4200/login-callback" },
                PostLogoutRedirectUris = { "http://localhost:4200/logout-callback", "https://localhost:4200/logout-callback" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "access_as_user"
                },
                AccessTokenLifetime = 900
            }
        };
}