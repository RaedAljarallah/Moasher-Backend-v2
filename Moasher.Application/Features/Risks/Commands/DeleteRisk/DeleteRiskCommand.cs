using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Risks.Commands.DeleteRisk;

public record DeleteRiskCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteRiskCommandHandler : IRequestHandler<DeleteRiskCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteRiskCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteRiskCommand request, CancellationToken cancellationToken)
    {
        var risk = await _context.InitiativeRisks
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        
        if (risk is null)
        {
            throw new NotFoundException();
        }
        
        _context.InitiativeRisks.Remove(risk);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}