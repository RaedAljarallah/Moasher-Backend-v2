using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Projects.Commands.DeleteProject;

public record DeleteProjectCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteProjectCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.InitiativeProjects
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project is null)
        {
            throw new NotFoundException();
        }

        _context.InitiativeProjects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}