using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeProjectConfiguration : ConfigurationBase<InitiativeProject>
{
    public override void Configure(EntityTypeBuilder<InitiativeProject> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.EstimatedAmount).HasPrecision(18, 6);
        builder.OwnsOne(p => p.Phase);
    }
}