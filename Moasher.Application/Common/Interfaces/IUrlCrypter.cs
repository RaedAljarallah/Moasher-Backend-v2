namespace Moasher.Application.Common.Interfaces;

public interface IUrlCrypter
{
    public string Encode(string arg);
    public string Decode(string arg);
}