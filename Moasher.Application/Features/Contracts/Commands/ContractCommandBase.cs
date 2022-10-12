namespace Moasher.Application.Features.Contracts.Commands;

public abstract record ContractCommandBase
{
    private string _name = default!;
    private string? _refNumber;
    private string? _supplier;

    public string Name { get => _name; set => _name = value.Trim(); }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal Amount { get; set; }
    public string? RefNumber { get => _refNumber; set => _refNumber = value?.Trim(); }
    public string? Supplier { get => _supplier; set => _supplier = value?.Trim(); }
    public bool CalculateAmount { get; set; }
    public Guid InitiativeId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid StatusEnumId { get; set; }
}