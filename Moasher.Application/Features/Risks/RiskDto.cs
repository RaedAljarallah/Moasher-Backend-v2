using Moasher.Application.Common.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Risks;

public record RiskDto : DtoBase
{
    public string Description { get; set; } = default!;
    public EnumValue Type { get; set; } = default!;
    public EnumValue Priority { get; set; } = default!;
    public EnumValue Probability { get; set; } = default!;
    public EnumValue Impact { get; set; } = default!;
    public string ImpactDescription { get; set; } = default!;
    public EnumValue Scope { get; set; } = default!;
    public string? Owner { get; set; }
    public string ResponsePlane { get; set; } = default!;
    public DateTimeOffset RaisedAt { get; set; }
    public string RaisedBy { get; set; } = default!;
    public string InitiativeName { get; set; } = default!;
    public string EntityName { get; set; } = default!;
}