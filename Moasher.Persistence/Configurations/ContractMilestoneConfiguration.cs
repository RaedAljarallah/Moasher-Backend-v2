using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class ContractMilestoneConfiguration : ConfigurationBase<ContractMilestone>
{
    public override void Configure(EntityTypeBuilder<ContractMilestone> builder)
    {
        base.Configure(builder);
        builder
            .HasOne(cm => cm.Milestone)
            .WithMany(m => m.ContractMilestones)
            .HasForeignKey(cm => cm.MilestoneId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(cm => cm.Contract)
            .WithMany(c => c.ContractMilestones)
            .HasForeignKey(cm => cm.ContractId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(cm => cm.Project)
            .WithMany(c => c.ContractMilestones)
            .HasForeignKey(cm => cm.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}