using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeProjectBaselineConfiguration : ConfigurationBase<InitiativeProjectBaseline>
{
    public override void Configure(EntityTypeBuilder<InitiativeProjectBaseline> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.InitialEstimatedAmount).HasPrecision(18, 6);
    }
}