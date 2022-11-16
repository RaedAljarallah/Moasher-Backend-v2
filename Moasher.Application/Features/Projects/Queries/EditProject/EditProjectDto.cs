using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.Milestones;
using Moasher.Application.Features.Projects.Commands;

namespace Moasher.Application.Features.Projects.Queries.EditProject;

public record EditProjectDto : ProjectCommandBase
{
    public Guid Id { get; set; }
    public EnumTypeDto Phase { get; set; } = default!;
    public IEnumerable<MilestoneDto> Milestones { get; set; } = Enumerable.Empty<MilestoneDto>();
}