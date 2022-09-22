using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeBudgetConfiguration : ConfigurationBase<InitiativeBudget>
{
    public override void Configure(EntityTypeBuilder<InitiativeBudget> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.Amount).HasPrecision(18, 6);
        builder.Property(i => i.InitialAmount).HasPrecision(18, 6);
    }
}