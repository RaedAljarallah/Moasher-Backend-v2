using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;
using Moasher.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "فريق المبادرة")]
public class InitiativeTeam : InitiativeRelatedDbEntity
{
    private EnumType _roleEnum = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public EnumValue Role { get; set; } = default!;

    [JsonIgnore]
    public EnumType RoleEnum
    {
        get => _roleEnum;
        set
        {
            _roleEnum = value;
            Role = new EnumValue(value.Name, value.Style);
        }
    }

    public Guid? RoleEnumId { get; set; }
}