using Moasher.Application.Common.Abstracts;
using Moasher.Application.Common.Services;
using Moasher.Application.Features.Expenditures;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Projects;

public record ProjectDto : DtoBase
{
    public string Name { get; set; } = default!;
    public DateTimeOffset PlannedBiddingDate { get; set; }
    public DateTimeOffset? ActualBiddingDate { get; set; }
    public DateTimeOffset PlannedContractingDate { get; set; }
    public decimal EstimatedAmount { get; set; }
    public ushort Duration { get; set; }
    public EnumValue Phase { get; set; } = default!;
    public string Status => GetStatus();

    private string GetStatus()
    {
        var statusList = new List<string>();
        if ((PlannedBiddingDate < DateTimeService.Now && !ActualBiddingDate.HasValue) 
            || (ActualBiddingDate > PlannedBiddingDate && PlannedContractingDate >= DateTimeService.Now))
        {
            statusList.Add("LateOnBidding");
        }

        if (PlannedContractingDate < DateTimeService.Now)
        {
            statusList.Add("LateOnContracting");
        }

        if (!statusList.Any())
        {
            statusList.Add("Ontrack");
        }
        
        return string.Join(",", statusList);
    }
}