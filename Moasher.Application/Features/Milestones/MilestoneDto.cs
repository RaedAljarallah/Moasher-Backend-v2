using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Services;

namespace Moasher.Application.Features.Milestones;

public record MilestoneDto : DtoBase, ISchedulableSummary
{
    public string Name { get; set; } = default!;
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public float Weight { get; set; }
    public string? SupportingDocument { get; set; }
    public string EntityName { get; set; } = default!;
    public string InitiativeName { get; set; } = default!;
    public Guid InitiativeId { get; set; }
    public string? Status => SchedulableEntityService.GetStatus(PlannedFinish, ActualFinish);
}