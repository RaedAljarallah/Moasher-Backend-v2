using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeConfiguration : ConfigurationBase<Initiative>
{
    public override void Configure(EntityTypeBuilder<Initiative> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.RequiredCost).HasPrecision(18, 6);
        builder.Property(i => i.ApprovedCost).HasPrecision(18, 6);
        builder.Property(i => i.CurrentYearBudget).HasPrecision(18, 6);
        builder.Property(i => i.TotalBudget).HasPrecision(18, 6);
        
        builder.OwnsOne(i => i.Status);
        builder.OwnsOne(i => i.FundStatus);
        
        builder.Ignore(i => i.LevelOneStrategicObjective);
        builder.Ignore(i => i.LevelTwoStrategicObjective);
        builder.Ignore(i => i.LevelFourStrategicObjective);
        builder.Ignore(i => i.LatestAnalyticsModel);
    }
}