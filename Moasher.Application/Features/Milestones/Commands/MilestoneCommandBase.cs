namespace Moasher.Application.Features.Milestones.Commands;

public abstract record MilestoneCommandBase
{
    private string _name = default!;
    private string? _supportingDocument;
    public string Name { get => _name; set => _name = value.Trim(); }
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public float Weight { get; set; }
    public Guid InitiativeId { get; set; }
    public string? SupportingDocument { get => _supportingDocument; set => _supportingDocument = value?.Trim(); }
}