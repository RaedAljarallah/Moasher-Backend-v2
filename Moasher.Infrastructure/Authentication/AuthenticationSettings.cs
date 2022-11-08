namespace Moasher.Infrastructure.Authentication;

public record AuthenticationSettings
{
    public string Authority { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Scope { get; set; } = default!;
}