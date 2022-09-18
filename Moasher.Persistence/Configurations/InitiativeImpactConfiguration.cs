using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeImpactConfiguration : ConfigurationBase<InitiativeImpact>
{
    public override void Configure(EntityTypeBuilder<InitiativeImpact> builder)
    {
        base.Configure(builder);
    }
}