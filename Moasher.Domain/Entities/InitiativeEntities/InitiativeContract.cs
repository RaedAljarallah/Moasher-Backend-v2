using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeContract : InitiativeRelatedDbEntity
{
    private EnumType _statusEnum = default!;
    
    public string Name { get; set; } = default!;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal Amount { get; set; }
    public string? RefNumber { get; set; }
    public EnumValue Status { get; private set; } = default!;
    public EnumType StatusEnum
    {
        get => _statusEnum;
        set
        {
            _statusEnum = value;
            Status = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? StatusEnumId { get; set; }
    
    public string? Supplier { get; set; }
    public bool CalculateAmount { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public InitiativeProject? Project { get; set; } = default!;
    // public ICollection<ContractMilestone> ContractMilestones { get; set; }
    //     = new HashSet<ContractMilestone>();
    //
}