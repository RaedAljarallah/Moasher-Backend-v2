using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeProject : InitiativeRelatedDbEntity
{
    private EnumType _phaseEnum = default!;
    
    public string Name { get; set; } = default!;
    public DateTimeOffset PlannedBiddingDate { get; set; }
    public DateTimeOffset? ActualBiddingDate { get; set; }
    public DateTimeOffset PlannedContractingDate { get; set; }
    public DateTimeOffset? ActualContractingDate { get; set; }
    public decimal EstimatedAmount { get; set; }
    public ushort Duration { get; set; }
    public EnumValue Phase { get; private set; } = default!;
    public EnumType PhaseEnum
    {
        get => _phaseEnum;
        set
        {
            _phaseEnum = value;
            Phase = new EnumValue(value.Name, value.Style);
        }
    }
    public Guid? PhaseEnumId { get; set; }
    public bool Contracted { get; set; }
    public ICollection<InitiativeExpenditure> Expenditures { get; set; }
        = new HashSet<InitiativeExpenditure>();

    public ICollection<InitiativeExpenditureBaseline> ExpendituresBaseline { get; set; }
        = new HashSet<InitiativeExpenditureBaseline>();
    
    public InitiativeContract? Contract { get; set; }
    public Guid? ContractId { get; set; }
}