using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Moasher.Domain.Enums;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeContract : InitiativeRelatedDbEntity
{
    private EnumType _typeEnum = default!;
    private EnumType _statusEnum = default!;
    
    public string Name { get; set; } = default!;
    public DateTimeOffset OfferingDate { get; set; }
    public DateTimeOffset ContractedDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal Amount { get; set; }
    public EnumValue Type { get; private set; } = default!;

    public EnumType TypeEnum
    {
        get => _typeEnum;
        set
        {
            _typeEnum = value;
            Type = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? TypeEnumId { get; set; }
    
    public string? RefNumber { get; set; }
    public ushort Duration { get; set; }
    public DurationUnit DurationUnit { get; set; }

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
    // public ICollection<ContractMilestone> ContractMilestones { get; set; }
    //     = new HashSet<ContractMilestone>();
    //
    // public ICollection<InitiativeContractExpenditure> Expenditures { get; set; }
    //     = new HashSet<InitiativeContractExpenditure>();
}