namespace Moasher.Authentication.Core.Identity.Entities;

public class InvalidToken
{
    public Guid Id { get; set; }
    public string Jti { get; set; } = default!;
    public DateTime Expiration { get; set; }

    public InvalidToken() { }

    public InvalidToken(string jti, long expiration)
    {
        Jti = jti;
        Expiration = DateTime.UnixEpoch.AddSeconds(expiration);
    }
}