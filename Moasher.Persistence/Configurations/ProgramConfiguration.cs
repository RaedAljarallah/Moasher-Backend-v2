using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class ProgramConfiguration : ConfigurationBase<Program>
{
    public override void Configure(EntityTypeBuilder<Program> builder)
    {
        base.Configure(builder);
    }
}