using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class KPIConfiguration : ConfigurationBase<KPI>
{
    public override void Configure(EntityTypeBuilder<KPI> builder)
    {
        base.Configure(builder);
        
        builder.Ignore(k => k.LevelOneStrategicObjective);
        builder.Ignore(k => k.LevelTwoStrategicObjective);
        builder.Ignore(k => k.LevelFourStrategicObjective);
        builder.Ignore(k => k.LatestAnalyticsModel);
    }
}