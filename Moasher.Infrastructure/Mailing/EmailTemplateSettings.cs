namespace Moasher.Infrastructure.Mailing;

public record class EmailTemplateSettings
{
    public string? BaseUrl { get; set; }
    public string? FirstLogoUrl { get; set; }
    public string? SecondLogoUrl { get; set; }
}