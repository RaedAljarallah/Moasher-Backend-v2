namespace Moasher.Infrastructure.Authentication.IdentityServer;

public record IdentityServerSettings
{
    public string Authority { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Scope { get; set; } = default!;
}