using Moasher.Authentication.Core.Identity.Entities;

namespace Moasher.Authentication.Core.Identity.Services;

public interface IInvalidToken
{
    public Task<int> CreateAsync(string jti, long exp, CancellationToken cancellationToken = new());
    public Task<List<InvalidToken>> GetExpiredTokens(int take, CancellationToken cancellationToken = new());
    public Task<int> RemoveExpiredTokens(IEnumerable<InvalidToken> expiredTokens,
        CancellationToken cancellationToken = new());
}