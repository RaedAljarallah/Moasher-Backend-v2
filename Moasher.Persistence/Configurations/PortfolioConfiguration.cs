using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class PortfolioConfiguration : ConfigurationBase<Portfolio>
{
    public override void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        base.Configure(builder);
    }
}