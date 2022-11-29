using Microsoft.EntityFrameworkCore;
using Moasher.Authentication.Core.Identity.Entities;
using Moasher.Authentication.Core.Persistence;

namespace Moasher.Authentication.Core.Identity.Services;

public class InvalidTokenService : IInvalidToken
{
    private readonly ApplicationDbContext _context;

    public InvalidTokenService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Task<int> CreateAsync(string tokenId, long expiration, CancellationToken cancellationToken = new())
    {
        var invalidToken = new InvalidToken(tokenId.Trim(), expiration);
        _context.InvalidTokens.Add(invalidToken);

        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<List<InvalidToken>> GetExpiredTokens(int take, CancellationToken cancellationToken = new())
    {
        return _context.InvalidTokens
            .Where(t => t.Expiration < DateTime.UtcNow)
            .OrderBy(t => t.Jti)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public Task<int> RemoveExpiredTokens(IEnumerable<InvalidToken> expiredTokens, CancellationToken cancellationToken = new())
    {
        _context.InvalidTokens.RemoveRange(expiredTokens);
         return _context.SaveChangesAsync(cancellationToken);
    }
}