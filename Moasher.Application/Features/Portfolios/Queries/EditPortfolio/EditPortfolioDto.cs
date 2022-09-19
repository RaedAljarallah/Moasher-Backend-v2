using Moasher.Application.Features.Initiatives;
using Moasher.Application.Features.Portfolios.Commands;

namespace Moasher.Application.Features.Portfolios.Queries.EditPortfolio;

public record EditPortfolioDto : PortfolioCommandBase
{
    public Guid Id { get; set; }
    public IEnumerable<InitiativeDto> RelatedInitiatives { get; set; } = Enumerable.Empty<InitiativeDto>();
}