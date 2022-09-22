namespace Moasher.Application.Features.ApprovedCosts.Commands;

public abstract record ApprovedCostCommandBase
{
    public DateTimeOffset ApprovalDate { get; set; }
    public decimal Amount { get; set; }
    public Guid InitiativeId { get; set; }
}