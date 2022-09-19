using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Interfaces;

namespace Moasher.Application.Features.Portfolios.Commands.DeletePortfolio;

public record DeletePortfolioCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, Unit>
{
    private readonly IMoasherDbContext _context;

    public DeletePortfolioCommandHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.Initiatives)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        
        if (portfolio is null)
        {
            throw new NotFoundException();
        }
        
        portfolio.Initiatives.ToList().ForEach(i => i.Portfolio = null);

        _context.Portfolios.Remove(portfolio);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}