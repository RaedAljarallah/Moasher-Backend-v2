﻿using System.ComponentModel.DataAnnotations.Schema;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Entities;

public class Entity : AuditableDbEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsOrganizer { get; set; }

    public ICollection<Initiative> Initiatives { get; set; }
        = new HashSet<Initiative>();
    public ICollection<KPI> KPIs { get; set; }
        = new HashSet<KPI>();
}