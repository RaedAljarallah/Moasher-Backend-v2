using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class EntityConfiguration : ConfigurationBase<Entity>
{
    public override void Configure(EntityTypeBuilder<Entity> builder)
    {
        base.Configure(builder);
    }
}