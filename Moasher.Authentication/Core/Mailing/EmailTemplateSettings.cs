namespace Moasher.Authentication.Core.Mailing;

public record EmailTemplateSettings
{
    public string? BaseUrl { get; set; }
    public string? FirstLogoUrl { get; set; }
    public string? SecondLogoUrl { get; set; }
}