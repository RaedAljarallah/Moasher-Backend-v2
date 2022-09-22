namespace Moasher.Application.Features.Portfolios.Commands;

public abstract record PortfolioCommandBase
{
    private string _code = default!;
    private string _name = default!;

    public string Code { get => _code; set => _code = value.Trim(); }
    public string Name { get => _name; set => _name = value.Trim(); }

    public IEnumerable<Guid> InitiativeIds { get; set; } = Enumerable.Empty<Guid>();
}