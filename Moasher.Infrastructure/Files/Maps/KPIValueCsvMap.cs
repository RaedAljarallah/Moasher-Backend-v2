using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.KPIValues;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class KPIValueCsvMap : ClassMap<KPIValueDto>
{
    public KPIValueCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(v => v.Audit!.CreatedAt).Ignore();
        Map(v => v.Audit!.CreatedBy).Ignore();
        Map(v => v.Audit!.LastModified).Ignore();
        Map(v => v.Audit!.LastModifiedBy).Ignore();
        Map(v => v.PlannedFinish).Convert(k => $"{k.Value.PlannedFinish:yyyy-MM-dd}");
        Map(v => v.ActualFinish)
            .Convert(v => v.Value.ActualFinish.HasValue ? $"{v.Value.ActualFinish.Value:yyyy-MM-dd}" : "");
    }
}