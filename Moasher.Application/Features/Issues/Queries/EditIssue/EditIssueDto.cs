using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.Issues.Commands;

namespace Moasher.Application.Features.Issues.Queries.EditIssue;

public record EditIssueDto : IssueCommandBase
{
    public Guid Id { get; set; }
    public EnumTypeDto Scope { get; set; } = default!;
    public EnumTypeDto Status { get; set; } = default!;
    public EnumTypeDto Impact { get; set; } = default!;
}