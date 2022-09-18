using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeMilestoneConfiguration : ConfigurationBase<InitiativeMilestone>
{
    public override void Configure(EntityTypeBuilder<InitiativeMilestone> builder)
    {
        base.Configure(builder);
    }
}