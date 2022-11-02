using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.KPIs;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class KPICsvMap : ClassMap<KPIDto>
{
    public KPICsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(k => k.Approved).Ignore();
        Map(k => k.Audit!.CreatedAt).Ignore();
        Map(k => k.Audit!.CreatedBy).Ignore();
        Map(k => k.Audit!.LastModified).Ignore();
        Map(k => k.Audit!.LastModifiedBy).Ignore();
        Map(k => k.Status!.Name).Name("Status_Name");
        Map(k => k.Status!.Style).Name("Status_Style");
        Map(k => k.StartDate).Convert(k => $"{k.Value.StartDate:yyyy-MM-dd}");
        Map(k => k.EndDate).Convert(k => $"{k.Value.EndDate:yyyy-MM-dd}");
    }
}