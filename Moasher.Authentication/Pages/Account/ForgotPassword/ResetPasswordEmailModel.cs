using Moasher.Authentication.Core.Mailing.Abstracts;

namespace Moasher.Authentication.Pages.Account.ForgotPassword;

public record ResetPasswordEmailModel(string FullName, string Url) : EmailModelBase
{
    private readonly string _url = Url;

    public string Url { get => GetUrl(); init => _url = value; }

    private string GetUrl()
    {
        var url = _url;
        if (url.StartsWith("/"))
        {
            url = url.Remove(0, 1);
        }

        return BaseUrl.EndsWith("/")
            ? $"{BaseUrl}{url}"
            : $"{BaseUrl}/{url}";
    }
}