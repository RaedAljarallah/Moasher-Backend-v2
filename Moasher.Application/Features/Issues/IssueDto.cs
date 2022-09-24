using Moasher.Application.Common.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Issues;

public record IssueDto : DtoBase
{
    public string Description { get; set; } = default!;
    public EnumValue Scope { get; set; } = default!;
    public EnumValue Status { get; set; } = default!;
    public EnumValue Impact { get; set; } = default!;
    public string ImpactDescription { get; set; } = default!;
    public string Source { get; set; } = default!;
    public string Reason { get; set; } = default!;
    public string Resolution { get; set; } = default!;
    public DateTimeOffset? EstimatedResolutionDate { get; set; }
    public DateTimeOffset RaisedAt { get; set; }
    public string RaisedBy { get; set; } = default!;
    public DateTimeOffset? ClosedAt { get; set; }
    public string InitiativeName { get; set; } = default!;
    public string EntityName { get; set; } = default!;
}