using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.StrategicObjectiveEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class StrategicObjectiveConfiguration : ConfigurationBase<StrategicObjective>
{
    public override void Configure(EntityTypeBuilder<StrategicObjective> builder)
    {
        base.Configure(builder);
    }
}