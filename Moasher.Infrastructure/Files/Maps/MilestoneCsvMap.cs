using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Milestones;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class MilestoneCsvMap : ClassMap<MilestoneDto>
{
    public MilestoneCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Approved).Ignore();
        Map(m => m.Audit!.CreatedAt).Ignore();
        Map(m => m.Audit!.CreatedBy).Ignore();
        Map(m => m.Audit!.LastModified).Ignore();
        Map(m => m.Audit!.LastModifiedBy).Ignore();
        Map(m => m.PlannedFinish).Convert(m => $"{m.Value.PlannedFinish:yyyy-MM-dd}");
        Map(m => m.ActualFinish)
            .Convert(m => m.Value.ActualFinish.HasValue ? $"{m.Value.ActualFinish.Value:yyyy-MM-dd}" : "");
    }
}