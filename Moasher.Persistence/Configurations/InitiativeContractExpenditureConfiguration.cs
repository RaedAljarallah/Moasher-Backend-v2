using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeContractExpenditureConfiguration : ConfigurationBase<InitiativeContractExpenditure>
{
    public override void Configure(EntityTypeBuilder<InitiativeContractExpenditure> builder)
    {
        base.Configure(builder);
        builder.Ignore(e => e.Initiative);
    }
}