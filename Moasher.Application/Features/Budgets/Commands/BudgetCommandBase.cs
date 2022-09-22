namespace Moasher.Application.Features.Budgets.Commands;

public abstract record BudgetCommandBase
{
    public DateTimeOffset ApprovalDate { get; set; }
    public decimal Amount { get; set; }
    public Guid InitiativeId { get; set; }
}