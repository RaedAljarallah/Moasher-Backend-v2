using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "فريق المبادرة")]
public class InitiativeTeam : InitiativeRelatedDbEntity
{
    private EnumType _roleEnum = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;

    public string RoleName { get; private set; } = default!;
    public string RoleStyle { get; private set; } = default!;
    
    [JsonIgnore]
    public EnumType RoleEnum
    {
        get => _roleEnum;
        set
        {
            _roleEnum = value;
            RoleName = value.Name;
            RoleStyle = value.Style;
        }
    }

    public Guid? RoleEnumId { get; set; }
}