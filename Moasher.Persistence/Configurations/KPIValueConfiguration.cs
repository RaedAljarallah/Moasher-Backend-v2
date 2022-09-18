using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class KPIValueConfiguration : ConfigurationBase<KPIValue>
{
    public override void Configure(EntityTypeBuilder<KPIValue> builder)
    {
        base.Configure(builder);
    }
}