using Moasher.Application.Features.EnumTypes;
using Moasher.Application.Features.Risks.Commands;

namespace Moasher.Application.Features.Risks.Queries.EditRisk;

public record EditRiskDto : RiskCommandBase
{
    public Guid Id { get; set; }
    public EnumTypeDto Type { get; set; } = default!;
    public EnumTypeDto Priority { get; set; } = default!;
    public EnumTypeDto Probability { get; set; } = default!;
    public EnumTypeDto Impact { get; set; } = default!;
    public EnumTypeDto Scope { get; set; } = default!;
}