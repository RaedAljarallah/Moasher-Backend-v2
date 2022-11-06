using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.UrlCrypter;

public class UrlCrypterService : IUrlCrypter
{
    public string Encode(string arg)
    {
        return  WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(arg));
    }

    public string Decode(string arg)
    {
        return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(arg));
    }
}