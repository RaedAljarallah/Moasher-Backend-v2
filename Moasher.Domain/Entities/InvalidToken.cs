namespace Moasher.Domain.Entities;

public class InvalidToken
{
    public Guid Id { get; set; }
    public string Jti { get; set; } = default!;
    public DateTime Expiration { get; set; }
}