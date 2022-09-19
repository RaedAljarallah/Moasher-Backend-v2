using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Portfolios;

namespace Moasher.Application.Features.Portfolios.EventHandlers;

public class PortfolioUpdatedEventHandler : INotificationHandler<PortfolioUpdatedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PortfolioUpdatedEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task Handle(PortfolioUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IMoasherDbContext>();
        var portfolioId = notification.Portfolio.Id;
        
        var portfolio = await context.Portfolios
            .Include(p => p.Initiatives)
            .FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);

        if (portfolio is not null)
        {
            portfolio.Initiatives.ToList().ForEach(i =>i.Portfolio = portfolio);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}