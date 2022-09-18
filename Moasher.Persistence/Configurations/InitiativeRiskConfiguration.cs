using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeRiskConfiguration : ConfigurationBase<InitiativeRisk>
{
    public override void Configure(EntityTypeBuilder<InitiativeRisk> builder)
    {
        base.Configure(builder);
        builder.OwnsOne(i => i.Type);
        builder.OwnsOne(i => i.Priority);
        builder.OwnsOne(i => i.Probability);
        builder.OwnsOne(i => i.Impact);
        builder.OwnsOne(i => i.Scope);
    }
}