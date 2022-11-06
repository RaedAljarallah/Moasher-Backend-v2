using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Moasher.Application.Common.Interfaces;
namespace Moasher.Infrastructure.UrlCrypter;

public class UrlCrypterService : IUrlCrypter
{
    public string Encode(string arg)
    {
        return Base64UrlEncoder.Encode(arg);
    }

    public string Decode(string arg)
    {
        return Base64UrlEncoder.Decode(arg);
    }
}