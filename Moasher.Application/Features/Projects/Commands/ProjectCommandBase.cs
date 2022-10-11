using Moasher.Application.Features.Expenditures.Commands.CreateExpenditure;

namespace Moasher.Application.Features.Projects.Commands;

public abstract record ProjectCommandBase
{
    private string _name = default!;
    
    public string Name { get => _name; set => _name = value.Trim(); }
    public DateTimeOffset PlannedBiddingDate { get; set; }
    public DateTimeOffset? ActualBiddingDate { get; set; }
    public DateTimeOffset PlannedContractingDate { get; set; }
    public decimal EstimatedAmount { get; set; }
    public ushort Duration { get; set; }
    public Guid InitiativeId { get; set; }
    public Guid PhaseEnumId { get; set; }
    public IEnumerable<CreateExpenditureCommand> Expenditures { get; set; } =
        Enumerable.Empty<CreateExpenditureCommand>();
}