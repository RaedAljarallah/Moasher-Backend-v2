using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Types;
using Moasher.Domain.ValueObjects;

namespace Moasher.Application.Features.Projects;

public record ProjectDto : DtoBase
{
    public string Name { get; set; } = default!;
    public DateTimeOffset PlannedBiddingDate { get; set; }
    public DateTimeOffset? ActualBiddingDate { get; set; }
    public DateTimeOffset PlannedContractingDate { get; set; }
    public DateTimeOffset PlannedContractEndDate { get; set; }
    public decimal EstimatedAmount { get; set; }
    public EnumValue Phase { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public string InitiativeName { get; set; } = default!;
    public Guid InitiativeId { get; set; }
    public string Status => GetStatus();

    private string GetStatus()
    {
        var statusList = new List<string>();
        if ((PlannedBiddingDate < LocalDateTime.Now && !ActualBiddingDate.HasValue) 
            || (ActualBiddingDate > PlannedBiddingDate && PlannedContractingDate >= LocalDateTime.Now))
        {
            statusList.Add("LateOnBidding");
        }

        if (PlannedContractingDate < LocalDateTime.Now)
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