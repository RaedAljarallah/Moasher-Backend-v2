using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeTeamConfiguration : ConfigurationBase<InitiativeTeam>
{
    public override void Configure(EntityTypeBuilder<InitiativeTeam> builder)
    {
        base.Configure(builder);
        builder.OwnsOne(t => t.Role);
    }
}