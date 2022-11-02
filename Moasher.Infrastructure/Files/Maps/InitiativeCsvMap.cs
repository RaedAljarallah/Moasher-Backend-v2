using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.Initiatives;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class InitiativeCsvMap : ClassMap<InitiativeDto>
{
    public InitiativeCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(i => i.Approved).Ignore();
        Map(i => i.Audit!.CreatedAt).Ignore();
        Map(i => i.Audit!.CreatedBy).Ignore();
        Map(i => i.Audit!.LastModified).Ignore();
        Map(i => i.Audit!.LastModifiedBy).Ignore();
        Map(i => i.Status!.Name).Name("Status_Name");
        Map(i => i.Status!.Style).Name("Status_Style");
        Map(i => i.FundStatus.Name).Name("FundStatus_Name");
        Map(i => i.FundStatus.Style).Name("FundStatus_Style");
        Map(i => i.PlannedStart).Convert(i => $"{i.Value.PlannedStart:yyyy-MM-dd}");
        Map(i => i.PlannedFinish).Convert(i => $"{i.Value.PlannedFinish:yyyy-MM-dd}");
        Map(i => i.ActualStart)
            .Convert(i => i.Value.ActualStart.HasValue ? $"{i.Value.ActualStart.Value:yyyy-MM-dd}" : "");
        Map(i => i.ActualFinish)
            .Convert(i => i.Value.ActualFinish.HasValue ? $"{i.Value.ActualFinish.Value:yyyy-MM-dd}" : "");
    }
}