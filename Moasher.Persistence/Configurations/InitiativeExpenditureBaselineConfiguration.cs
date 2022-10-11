using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeExpenditureBaselineConfiguration : ConfigurationBase<InitiativeExpenditureBaseline>
{
    public override void Configure(EntityTypeBuilder<InitiativeExpenditureBaseline> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.InitialPlannedAmount).HasPrecision(18, 6);
    }
}