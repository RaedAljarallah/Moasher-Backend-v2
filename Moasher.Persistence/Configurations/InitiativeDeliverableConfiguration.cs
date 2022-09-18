using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeDeliverableConfiguration : ConfigurationBase<InitiativeDeliverable>
{
    public override void Configure(EntityTypeBuilder<InitiativeDeliverable> builder)
    {
        base.Configure(builder);
    }
}