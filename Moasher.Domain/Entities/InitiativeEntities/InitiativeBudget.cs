﻿using Moasher.Domain.Entities.InitiativeEntities.Abstracts;

namespace Moasher.Domain.Entities.InitiativeEntities;

public class InitiativeBudget : InitiativeRelatedDbEntity
{
    public DateTimeOffset ApprovalDate { get; set; }
    public decimal Amount { get; set; }
    public decimal InitialAmount { get; set; }
    public string? SupportingDocument { get; set; }
}