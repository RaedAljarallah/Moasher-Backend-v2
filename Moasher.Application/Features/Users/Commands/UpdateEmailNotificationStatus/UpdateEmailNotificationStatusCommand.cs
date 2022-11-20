using MediatR;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Users.Commands.UpdateEmailNotificationStatus;

public record UpdateEmailNotificationStatusCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public bool Enable { get; set; }
}

public class UpdateEmailNotificationStatusCommandHandler : IRequestHandler<UpdateEmailNotificationStatusCommand, bool>
{
    private readonly IIdentityService _identityService;

    public UpdateEmailNotificationStatusCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<bool> Handle(UpdateEmailNotificationStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException();
        }
        
        var result = await _identityService.UpdateEmailNotificationStatusAsync(user, request.Enable, cancellationToken);
        return result;
    }
}