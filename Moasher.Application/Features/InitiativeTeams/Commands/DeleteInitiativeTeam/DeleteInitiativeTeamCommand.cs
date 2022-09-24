using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.InitiativeTeams.Commands.DeleteInitiativeTeam;

public record DeleteInitiativeTeamCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteInitiativeTeamCommandHandler : IRequestHandler<DeleteInitiativeTeamCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteInitiativeTeamCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteInitiativeTeamCommand request, CancellationToken cancellationToken)
    {
        var member = await _context.InitiativeTeams
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
        
        if (member is null)
        {
            throw new NotFoundException();
        }
        
        _context.InitiativeTeams.Remove(member);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}