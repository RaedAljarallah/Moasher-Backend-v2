using Moasher.Application.Features.Initiatives.Queries.GetInitiativesStatusProgress;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.KPIs.Queries.GetKPIsStatusProgress;

public record KPIsStatusProgressDto
{
    public int Year { get; set; }
    public Month Month { get; set; }
    public List<StatusProgressDto> Progress { get; set; } = new();
}