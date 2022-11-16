using Moasher.Application.Features.Expenditures.Commands.CreateProjectExpenditure;

namespace Moasher.Application.Features.Projects.Commands;

public abstract record ProjectCommandBase
{
    private string _name = default!;
    
    public string Name { get => _name; set => _name = value.Trim(); }
    public DateTimeOffset PlannedBiddingDate { get; set; }
    public DateTimeOffset? ActualBiddingDate { get; set; }
    public DateTimeOffset PlannedContractingDate { get; set; }
    public DateTimeOffset PlannedContractEndDate { get; set; }
    public decimal EstimatedAmount { get; set; }
    public Guid InitiativeId { get; set; }
    public Guid PhaseEnumId { get; set; }
    public IEnumerable<CreateProjectExpenditureCommand> Expenditures { get; set; } =
        Enumerable.Empty<CreateProjectExpenditureCommand>();

    public IEnumerable<Guid> MilestoneIds { get; set; } = Enumerable.Empty<Guid>();
}