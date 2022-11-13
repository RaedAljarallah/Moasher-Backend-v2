using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities.EditRequests;

namespace Moasher.Persistence.Configurations;

public class EditRequestConfiguration : IEntityTypeConfiguration<EditRequest>
{
    public void Configure(EntityTypeBuilder<EditRequest> builder)
    {
        builder.Property<int>("CodeInc")
            .HasDefaultValueSql("NEXT VALUE FOR dbo.ERCodeSequence");
        
        builder.Property(e => e.Code)
            .HasComputedColumnSql("('ER-'+right(replicate('0',(5))+CONVERT([varchar],[CodeInc]),(5)))");
        
        builder.HasIndex(er => er.Code).IsUnique();
    }
}