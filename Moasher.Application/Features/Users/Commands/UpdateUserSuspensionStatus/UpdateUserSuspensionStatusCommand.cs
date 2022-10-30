using MediatR;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Users.Commands.UpdateUserSuspensionStatus;

public record UpdateUserSuspensionStatusCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public bool Suspend { get; set; }
}

public class UpdateUserSuspensionStatusCommandHandler : IRequestHandler<UpdateUserSuspensionStatusCommand, bool>
{
    private readonly IIdentityService _identityService;

    public UpdateUserSuspensionStatusCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<bool> Handle(UpdateUserSuspensionStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException();
        }
        
        var result = await _identityService.UpdateUserSuspensionStatusAsync(user, request.Suspend, cancellationToken);
        return result;
    }
}