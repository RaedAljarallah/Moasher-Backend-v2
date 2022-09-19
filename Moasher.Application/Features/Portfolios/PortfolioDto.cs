using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Portfolios;

public record PortfolioDto : DtoBase
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int InitiativesCount { get; set; }
}