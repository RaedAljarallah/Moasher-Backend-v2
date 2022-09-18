using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeContractConfiguration : ConfigurationBase<InitiativeContract>
{
    public override void Configure(EntityTypeBuilder<InitiativeContract> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.Amount).HasPrecision(18, 6);
        builder.OwnsOne(c => c.Type);
        builder.OwnsOne(c => c.Status);
    }
}