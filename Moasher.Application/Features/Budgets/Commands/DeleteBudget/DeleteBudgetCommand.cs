using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Budgets;

namespace Moasher.Application.Features.Budgets.Commands.DeleteBudget;

public record DeleteBudgetCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteBudgetCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _context.InitiativeBudgets
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (budget is null)
        {
            throw new NotFoundException();
        }
        
        budget.AddDomainEvent(new BudgetDeletedEvent(budget));
        _context.InitiativeBudgets.Remove(budget);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
