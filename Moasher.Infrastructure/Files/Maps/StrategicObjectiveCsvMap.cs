using System.Globalization;
using CsvHelper.Configuration;
using Moasher.Application.Features.StrategicObjectives;

namespace Moasher.Infrastructure.Files.Maps;

public sealed class StrategicObjectiveCsvMap : ClassMap<StrategicObjectiveDtoBase>
{
    public StrategicObjectiveCsvMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(o => o.Approved).Ignore();
        Map(o => o.Audit!.CreatedAt).Ignore();
        Map(o => o.Audit!.CreatedBy).Ignore();
        Map(o => o.Audit!.LastModified).Ignore();
        Map(o => o.Audit!.LastModifiedBy).Ignore();
        Map(o => o.HierarchyId).Ignore();
        Map(o => o.InitiativesCount).Ignore();
        Map(o => o.KPIsCount).Ignore();
    }
}