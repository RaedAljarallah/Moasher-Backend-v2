using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Portfolios;

namespace Moasher.Application.Features.Portfolios.EventHandlers;

public class PortfolioUpdatedEventHandler : INotificationHandler<PortfolioUpdatedEvent>
{
    private readonly IMoasherDbContext _context;

    public PortfolioUpdatedEventHandler(IMoasherDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(PortfolioUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var portfolioId = notification.Portfolio.Id;
        
        var portfolio = await _context.Portfolios
            .Include(p => p.Initiatives)
            .FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);

        if (portfolio is not null)
        {
            portfolio.Initiatives.ToList().ForEach(i => i.Portfolio = portfolio);
            _context.Initiatives.UpdateRange(portfolio.Initiatives);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}