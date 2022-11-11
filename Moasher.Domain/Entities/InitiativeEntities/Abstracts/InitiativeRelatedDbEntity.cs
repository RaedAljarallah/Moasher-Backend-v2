using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Interfaces;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities.Abstracts;

public abstract class InitiativeRelatedDbEntity : ApprovableDbEntity
{
    private Initiative _initiative = default!;
    
    [Display(Name = "اسم المبادرة")]
    public string InitiativeName { get; private set; } = default!;
    
    [JsonIgnore]
    public Initiative Initiative
    {
        get => _initiative;
        set
        {
            _initiative = value;
            InitiativeName = value.Name;
            EntityName = value.EntityName;
        }
    }
    
    [Display(Name = "معرف المبادرة")]
    public Guid InitiativeId { get; set; }
    
    [Display(Name = "اسم الجهة")]
    public string EntityName { get; private set; } = default!;
}