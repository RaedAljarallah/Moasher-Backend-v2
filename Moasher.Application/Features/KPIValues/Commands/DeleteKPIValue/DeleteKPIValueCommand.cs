using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.KPIValues;

namespace Moasher.Application.Features.KPIValues.Commands.DeleteKPIValue;

public record DeleteKPIValueCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteKPIValueCommandHandler : IRequestHandler<DeleteKPIValueCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteKPIValueCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteKPIValueCommand request, CancellationToken cancellationToken)
    {
        var value = await _context.KPIValues
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (value is null)
        {
            throw new NotFoundException();
        }
        
        value.AddDomainEvent(new KPIValueDeletedEvent(value));
        _context.KPIValues.Remove(value);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}