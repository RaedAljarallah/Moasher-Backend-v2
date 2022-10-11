using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeExpenditureConfiguration : ConfigurationBase<InitiativeExpenditure>
{
    public override void Configure(EntityTypeBuilder<InitiativeExpenditure> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.PlannedAmount).HasPrecision(18, 6);
        builder.Property(e => e.ActualAmount).HasPrecision(18, 6);
    }
}