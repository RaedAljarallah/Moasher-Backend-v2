using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Entities;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class EntityCsvMap : ClassMap<EntityDto>
{
    public EntityCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(e => e.Approved).Ignore();
        Map(e => e.Audit!.CreatedAt).Ignore();
        Map(e => e.Audit!.CreatedBy).Ignore();
        Map(e => e.Audit!.LastModified).Ignore();
        Map(e => e.Audit!.LastModifiedBy).Ignore();
    }
}