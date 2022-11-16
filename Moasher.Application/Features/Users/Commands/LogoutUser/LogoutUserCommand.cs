using MediatR;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;

namespace Moasher.Application.Features.Users.Commands.LogoutUser;

public record LogoutUserCommand : IRequest<Unit>
{
    private string _jti = default!;

    public string Jti { get => _jti; set => _jti = value.Trim(); }

    public long Expiration { get; set; }
}

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public LogoutUserCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Jti))
        {
            return Unit.Value;
        }

        var invalidToken = new InvalidToken(request.Jti, request.Expiration);
        _context.InvalidTokens.Add(invalidToken);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}