using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Contracts;

namespace Moasher.Application.Features.Contracts.Commands.DeleteContract;

public record DeleteContractCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteContractCommandHandler : IRequestHandler<DeleteContractCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteContractCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _context.InitiativeContracts
            .Include(c => c.Project)
            .Include(c => c.Expenditures)
            .Include(c => c.ExpendituresBaseline)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (contract is null)
        {
            throw new NotFoundException();
        }
        
        _context.InitiativeExpenditures.RemoveRange(contract.Expenditures);
        _context.InitiativeExpendituresBaseline.RemoveRange(contract.ExpendituresBaseline);
        _context.InitiativeProjects.Remove(contract.Project);
        _context.InitiativeContracts.Remove(contract);
        contract.AddDomainEvent(new ContractDeletedEvent(contract));
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}