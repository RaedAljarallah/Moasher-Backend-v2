using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeProjectProgress : ApprovableDbEntity
{
    private EnumType _phaseEnum = default!;
    
    [Display(Name = "المرحلة")]
    public string PhaseName { get; private set; } = default!;
    public string PhaseStyle { get; private set; } = default!;
    
    [JsonIgnore]
    public EnumType PhaseEnum
    {
        get => _phaseEnum;
        set
        {
            _phaseEnum = value;
            PhaseName = value.Name;
            PhaseStyle = value.Style;
        }
    }
    public Guid? PhaseEnumId { get; set; }
    public DateTimeOffset PhaseStartedAt { get; set; }
    public DateTimeOffset? PhaseEndedAt { get; set; }
    public string PhaseStartedBy { get; set; } = default!;
    public string? PhaseEndedBy { get; set; }
    public bool Completed { get; set; }
    [JsonIgnore]
    public InitiativeProject Project { get; set; } = default!;
    public Guid ProjectId { get; set; }
    
    public static InitiativeProjectProgress CreateProjectProgressItem(InitiativeProject project)
    {
        return new InitiativeProjectProgress
        {
            PhaseEnum = project.PhaseEnum,
        };
    }
    
    public void Complete()
    {
        Completed = true;
    } 
}