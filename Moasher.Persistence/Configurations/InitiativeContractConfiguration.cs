using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeContractConfiguration : ConfigurationBase<InitiativeContract>
{
    public override void Configure(EntityTypeBuilder<InitiativeContract> builder)
    {
        base.Configure(builder);
        
        builder.Property(c => c.Amount).HasPrecision(18, 6);
        builder.Property(c => c.TotalExpenditure).HasPrecision(18, 6);
        builder.Property(c => c.CurrentYearExpenditure).HasPrecision(18, 6);
    }
}