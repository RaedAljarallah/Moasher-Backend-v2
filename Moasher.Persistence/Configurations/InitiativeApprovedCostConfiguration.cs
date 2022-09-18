using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeApprovedCostConfiguration : ConfigurationBase<InitiativeApprovedCost>
{
    public override void Configure(EntityTypeBuilder<InitiativeApprovedCost> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.Amount).HasPrecision(18, 6);
    }
}