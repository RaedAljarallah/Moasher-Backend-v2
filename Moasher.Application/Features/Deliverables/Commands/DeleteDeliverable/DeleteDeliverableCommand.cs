using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Deliverables.Commands.DeleteDeliverable;

public record DeleteDeliverableCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteDeliverableCommandHandler : IRequestHandler<DeleteDeliverableCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteDeliverableCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteDeliverableCommand request, CancellationToken cancellationToken)
    {
        var deliverable = await _context.InitiativeDeliverables
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        
        if (deliverable is null)
        {
            throw new NotFoundException();
        }
        
        _context.InitiativeDeliverables.Remove(deliverable);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
