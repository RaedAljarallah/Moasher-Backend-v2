using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Initiatives;

namespace Moasher.Application.Features.Initiatives.Commands.DeleteInitiative;

public record DeleteInitiativeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteInitiativeCommandHandler : IRequestHandler<DeleteInitiativeCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteInitiativeCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteInitiativeCommand request, CancellationToken cancellationToken)
    {
        var initiative = await _context.Initiatives
            .Include(i => i.Analytics)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
        if (initiative is null)
        {
            throw new NotFoundException();
        }
        
        initiative.AddDomainEvent(new InitiativeDeletedEvent(initiative));
        _context.Analytics.RemoveRange(initiative.Analytics);
        _context.Initiatives.Remove(initiative);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}