﻿using System.ComponentModel.DataAnnotations;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

[Display(Name = "مخرجات")]
public class InitiativeDeliverable : InitiativeRelatedDbEntity, ISchedulable
{
    public string Name { get; set; } = default!;
    public DateTimeOffset PlannedFinish { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public string? SupportingDocument { get; set; }
}