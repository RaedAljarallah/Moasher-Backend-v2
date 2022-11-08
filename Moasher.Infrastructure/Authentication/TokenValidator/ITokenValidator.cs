namespace Moasher.Infrastructure.Authentication.TokenValidator;

public interface ITokenValidator
{
    public Task<bool> IsValid(string jti, CancellationToken cancellationToken = default);
}