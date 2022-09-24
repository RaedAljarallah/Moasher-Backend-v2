using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Issues.Commands.DeleteIssue;

public record DeleteIssueCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeleteIssueCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
    {
        var issue = await _context.InitiativeIssues
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
        
        if (issue is null)
        {
            throw new NotFoundException();
        }
        
        _context.InitiativeIssues.Remove(issue);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
