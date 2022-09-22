using Moasher.Domain.Common.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities.Abstracts;

public abstract class InitiativeRelatedDbEntity : AuditableDbEntity
{
    private Initiative _initiative = default!;
    public string InitiativeName { get; private set; } = default!;

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
    public Guid InitiativeId { get; set; }
    public string EntityName { get; private set; } = default!;
}