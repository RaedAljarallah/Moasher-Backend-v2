using MediatR;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Users.Commands.ResetUserPassword;

public record ResetUserPasswordCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public ResetUserPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException();
        }

        await _identityService.ResetUserPassword(user, cancellationToken);
        
        return Unit.Value;
    }
}