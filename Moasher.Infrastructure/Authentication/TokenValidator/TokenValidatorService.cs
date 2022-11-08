using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Infrastructure.Authentication.TokenValidator;

public class TokenValidatorService : ITokenValidator
{
    private readonly IMoasherDbContext _context;

    public TokenValidatorService(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> IsValid(string jti, CancellationToken cancellationToken)
    {
        return !await _context.InvalidTokens.AnyAsync(t => t.Jti == jti, cancellationToken);
    }
}