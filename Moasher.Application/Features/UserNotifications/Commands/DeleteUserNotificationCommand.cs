using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.UserNotifications.Commands;

public record DeleteUserNotificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteUserNotificationCommandHandler : IRequestHandler<DeleteUserNotificationCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteUserNotificationCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteUserNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.UserNotifications
            .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        
        if (notification is null)
        {
            throw new NotFoundException();
        }

        _context.UserNotifications.Remove(notification);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}