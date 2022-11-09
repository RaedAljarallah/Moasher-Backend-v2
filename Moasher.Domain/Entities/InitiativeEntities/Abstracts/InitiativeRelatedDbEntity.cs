﻿using Moasher.Domain.Common.Abstracts;
using Newtonsoft.Json;

namespace Moasher.Domain.Entities.InitiativeEntities.Abstracts;

public abstract class InitiativeRelatedDbEntity : ApprovableDbEntity
{
    private Initiative _initiative = default!;
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
    public Guid InitiativeId { get; set; }
    public string EntityName { get; private set; } = default!;
}