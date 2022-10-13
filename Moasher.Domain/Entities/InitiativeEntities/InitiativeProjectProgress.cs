using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.ValueObjects;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeProjectProgress : DbEntity
{
    private EnumType _phaseEnum = default!;
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
    public DateTimeOffset PhaseStartedAt { get; set; }
    public DateTimeOffset? PhaseEndedAt { get; set; }
    public string PhaseStartedBy { get; set; } = default!;
    public string? PhaseEndedBy { get; set; }
    public bool Completed { get; set; }
    public InitiativeProject Project { get; set; } = default!;
    public Guid ProjectId { get; set; }
    
    public static InitiativeProjectProgress CreateProjectProgressItem(InitiativeProject project)
    {
        return new InitiativeProjectProgress
        {
            PhaseEnum = project.PhaseEnum,
            PhaseStartedAt = project.CreatedAt,
            PhaseStartedBy = project.CreatedBy
        };
    }
    
    public void Complete(DateTimeOffset completedAt, string completedBy)
    {
        Completed = true;
        PhaseEndedAt = completedAt;
        PhaseEndedBy = completedBy;
    } 
}