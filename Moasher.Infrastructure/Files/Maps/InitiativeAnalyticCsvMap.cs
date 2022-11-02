using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Analytics;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class InitiativeAnalyticCsvMap : ClassMap<AnalyticDto>
{
    public InitiativeAnalyticCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(a => a.Approved).Ignore();
        Map(a => a.Audit!.CreatedAt).Ignore();
        Map(a => a.Audit!.CreatedBy).Ignore();
        Map(a => a.Audit!.LastModified).Ignore();
        Map(a => a.Audit!.LastModifiedBy).Ignore();
        Map(a => a.AnalyzedAt).Convert(a => $"{a.Value.AnalyzedAt:yyyy-MM-dd}");
        Map(a => a.KPIName).Ignore();
        Map(a => a.KPIId).Ignore();
    }
}