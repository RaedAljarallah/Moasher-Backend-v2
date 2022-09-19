using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Events.Portfolios;

public class PortfolioUpdatedEvent : DomainEvent
{
    public Portfolio Portfolio { get; }

    public PortfolioUpdatedEvent(Portfolio portfolio)
    {
        Portfolio = portfolio;
    }
}