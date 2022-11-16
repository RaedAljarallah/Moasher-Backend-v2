using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.UserNotifications.Commands;

public record UpdateUserNotificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public bool Read { get; set; }
}

public class UpdateUserNotificationCommandHandler : IRequestHandler<UpdateUserNotificationCommand, Unit>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public UpdateUserNotificationCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateUserNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.UserNotifications
            .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

        if (notification is null)
        {
            throw new NotFoundException();
        }

        if (notification.HasRead != request.Read)
        {
            notification.HasRead = request.Read;
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        return Unit.Value;
    }
}