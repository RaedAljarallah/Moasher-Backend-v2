using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Services;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Projects;

public record ProjectDto : DtoBase
{
    public string Name { get; set; } = default!;
    public DateTimeOffset PlannedBiddingDate { get; set; }
    public DateTimeOffset? ActualBiddingDate { get; set; }
    public DateTimeOffset PlannedContractingDate { get; set; }
    public decimal EstimatedAmount { get; set; }
    public EnumValue Phase { get; set; } = default!;
    public string Status => GetStatus();

    private string GetStatus()
    {
        if ((PlannedBiddingDate < DateTimeService.Now && !ActualBiddingDate.HasValue) 
            || (ActualBiddingDate > PlannedBiddingDate && PlannedContractingDate >= DateTimeService.Now))
        {
            return "LateOnBidding";
        }

        if (PlannedContractingDate < DateTimeService.Now)
        {
            return "LateOnContracting";
        }
        

        return "Ontrack";
    }
}