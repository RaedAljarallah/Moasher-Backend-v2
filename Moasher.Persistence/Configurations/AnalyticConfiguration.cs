using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class AnalyticConfiguration :  ConfigurationBase<Analytic>
{
    public override void Configure(EntityTypeBuilder<Analytic> builder)
    {
        base.Configure(builder);
    }
}