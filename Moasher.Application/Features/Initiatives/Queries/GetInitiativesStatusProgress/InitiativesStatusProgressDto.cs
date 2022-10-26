using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Initiatives.Queries.GetInitiativesStatusProgress;

public record InitiativesStatusProgressDto
{
    public int Year { get; set; }
    public Month Month { get; set; }
    public List<StatusProgressDto> Progress { get; set; } = new();
}

public record StatusProgressDto
{
    public EnumValue Status { get; set; } = default!;
    public int Count { get; set; }
}