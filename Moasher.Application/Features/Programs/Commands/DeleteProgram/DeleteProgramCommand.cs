using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Programs.Commands.DeleteProgram;

public record DeleteProgramCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}

public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteProgramCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _context.Programs.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (program is null)
        {
            throw new NotFoundException();
        }

        _context.Programs.Remove(program);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}