using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.ApprovedCosts;

namespace Moasher.Application.Features.ApprovedCosts.Commands.DeleteApprovedCost;

public record DeleteApprovedCostCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteApprovedCostCommandHandler : IRequestHandler<DeleteApprovedCostCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteApprovedCostCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteApprovedCostCommand request, CancellationToken cancellationToken)
    {
        var approvedCost = await _context.InitiativeApprovedCosts
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (approvedCost is null)
        {
            throw new NotFoundException();
        }
        
        approvedCost.AddDomainEvent(new ApprovedCostDeletedEvent(approvedCost));
        _context.InitiativeApprovedCosts.Remove(approvedCost);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}