using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Persistence.Configurations;

public class InitiativeProjectProgressConfiguration: IEntityTypeConfiguration<InitiativeProjectProgress>
{
    public void Configure(EntityTypeBuilder<InitiativeProjectProgress> builder)
    {
        builder.Ignore(p => p.DomainEvents);
        builder.Property(e => e.PhaseStartedBy).HasMaxLength(256).IsRequired();
        builder.Property(e => e.PhaseStartedAt).IsRequired();
        builder.Property(e => e.PhaseEndedBy).HasMaxLength(256);
    }
}