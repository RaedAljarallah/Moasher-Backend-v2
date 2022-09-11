using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moasher.Domain.Entities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class EnumTypeConfiguration : ConfigurationBase<EnumType>
{
    public override void Configure(EntityTypeBuilder<EnumType> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.Metadata)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                v => JsonSerializer.Deserialize<IDictionary<string, string>>(v, (JsonSerializerOptions)null!),
                new ValueComparer<IDictionary<string, string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c));
    }
}