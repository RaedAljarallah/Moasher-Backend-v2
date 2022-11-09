namespace Moasher.Authentication.Core.Mailing.Abstracts;

public abstract record EmailModelBase
{
    public string BaseUrl { get; set; } = default!;
    public string FirstLogoUrl { get; set; } = default!;
    public string SecondLogoUrl { get; set; } = default!;
}