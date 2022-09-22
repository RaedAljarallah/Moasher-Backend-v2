using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Budgets;

public record BudgetDto : DtoBase
{
    public DateTimeOffset ApprovalDate { get; set; }
    public decimal Amount { get; set; }
    public decimal InitialAmount { get; set; }
    public string? SupportingDocument { get; set; }
    public string InitiativeName { get; set; } = default!;
    public Guid InitiativeId { get; set; }
}