using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class InitiativeIssueConfiguration : ConfigurationBase<InitiativeIssue>
{
    public override void Configure(EntityTypeBuilder<InitiativeIssue> builder)
    {
        base.Configure(builder);
        builder.OwnsOne(i => i.Scope);
        builder.OwnsOne(i => i.Status);
        builder.OwnsOne(i => i.Impact);
    }
}