using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Entities.Commands.DeleteEntity;

public record DeleteEntityCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}

public class DeleteEntityCommandHandler : IRequestHandler<DeleteEntityCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteEntityCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Entities.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException();
        }

        _context.Entities.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}