using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Deliverables;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class DeliverableCsvMap : ClassMap<DeliverableDto>
{
    public DeliverableCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(d => d.Approved).Ignore();
        Map(d => d.Audit!.CreatedAt).Ignore();
        Map(d => d.Audit!.CreatedBy).Ignore();
        Map(d => d.Audit!.LastModified).Ignore();
        Map(d => d.Audit!.LastModifiedBy).Ignore();
        Map(d => d.PlannedFinish).Convert(d => $"{d.Value.PlannedFinish:yyyy-MM-dd}");
        Map(d => d.ActualFinish)
            .Convert(d => d.Value.ActualFinish.HasValue ? $"{d.Value.ActualFinish.Value:yyyy-MM-dd}" : "");
    }
}